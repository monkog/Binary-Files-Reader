using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BinaryFilesReader.Properties;

namespace BinaryFilesReader
{
	public partial class Browser : Form
	{
		private readonly Dictionary<string, DecompiledAssembly> _assemblies = new Dictionary<string, DecompiledAssembly>();
		private Assembly _selectedAssembly;
		private string _selectedFullTypeName;

		public Browser()
		{
			InitializeComponent();

			treeView.ImageList = IconsStyle.Icons2017;
			listView.SmallImageList = IconsStyle.Icons2017;
		}

		private void LoadFileClicked(object sender, EventArgs e)
		{
			var path = SelectAssemblyPath();
			if (string.IsNullOrEmpty(path) || _assemblies.ContainsKey(path)) return;

			if (!TryOpenAssembly(path, out var assembly)) return;
			_assemblies[path] = assembly;
			CreateAssemblyTree(path, assembly);
			buttonCreate.Enabled = false;
		}

		private static bool TryOpenAssembly(string path, out DecompiledAssembly assembly)
		{
			assembly = null;

			try
			{
				assembly = new DecompiledAssembly(path);
			}
			catch (FileLoadException exception)
			{
				var text = string.Format(Resources.CannotLoadAssembly, path, exception.Message + exception.InnerException?.Message);
				MessageBox.Show(text, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			catch (BadImageFormatException exception)
			{
				var text = string.Format(Resources.InvalidAssemblyOrCLRVersion, path, exception.Message + exception.InnerException?.Message);
				MessageBox.Show(text, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			catch (ReflectionTypeLoadException exception)
			{
				var text = string.Format(Resources.TypesCouldNotBeLoaded, path, exception.Message + exception.InnerException?.Message);
				MessageBox.Show(text, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			return true;
		}

		private void CreateAssemblyTree(string path, DecompiledAssembly assembly)
		{
			var root = treeView.Nodes.Add(path, path.Substring(path.LastIndexOf('\\') + 1), 0, 0);

			foreach (var assemblyType in assembly.Types.Keys)
			{
				var typeRoot = root;
				var typeNameParts = assemblyType.Split('.');

				foreach (var type in typeNameParts)
				{
					var foundNodes = typeRoot.Nodes.Find(type, false);
					if (foundNodes.Any())
					{
						typeRoot = foundNodes.Single();
						continue;
					}

					typeRoot = InitializeTypeNode(typeRoot, type, assembly.Assembly);
				}
			}
		}

		private static TreeNode InitializeTypeNode(TreeNode root, string typeName, Assembly assembly)
		{
			var node = root.Nodes.Add(typeName);
			node.Name = typeName;

			var objPath = GetFullTypeName(node.FullPath);
			try
			{
				var type = assembly.GetType(objPath);
				node.ImageIndex = IconsStyle.GetTypeImageIndex(type);
				node.SelectedImageIndex = node.ImageIndex;
			}
			catch (NotSupportedException)
			{
				root.Nodes.Remove(node);
			}

			return node;
		}

		private static string SelectAssemblyPath()
		{
			var openFileDialog = new OpenFileDialog { Filter = Resources.SupportedAssemblyExtensions };
			openFileDialog.ShowDialog();
			return openFileDialog.FileName;
		}

		private void AssemblyObjectSelected(object sender, TreeViewEventArgs e)
		{
			var pathParts = e.Node.FullPath.Split('\\');
			if (pathParts.Length == 1) return;

			var root = FindRootForType(pathParts);
			buttonCreate.Enabled = false;
			listView.Items.Clear();

			var assembly = _assemblies[root.Name];
			_selectedAssembly = assembly.Assembly;

			var fullTypeName = GetFullTypeName(e.Node.FullPath);
			if (!assembly.Types.ContainsKey(fullTypeName)) return;

			var type = assembly.Types[fullTypeName];
			if (!type.IsClass && !type.IsInterface) return;

			_selectedFullTypeName = fullTypeName;
			var constructor = type.GetConstructor(Type.EmptyTypes);
			if (constructor != null && !type.IsAbstract)
				buttonCreate.Enabled = true;

			var fields = InitializeFieldItems(assembly, type);
			fields.Sort(new ListViewItemComparer().Compare);
			listView.Items.AddRange(fields.ToArray());

			var methods = InitializeMethodItems(assembly, type);
			methods.Sort(new ListViewItemComparer().Compare);
			listView.Items.AddRange(methods.ToArray());

			var events = InitializeEventItems(assembly, type);
			events.Sort(new ListViewItemComparer().Compare);
			listView.Items.AddRange(events.ToArray());
		}

		private TreeNode FindRootForType(string[] pathParts)
		{
			TreeNode root = null;
			foreach (TreeNode treeNode in treeView.Nodes)
				if (treeNode.Text == pathParts[0])
				{
					root = treeNode;
					break;
				}

			return root;
		}

		private static List<ListViewItem> InitializeMethodItems(DecompiledAssembly assembly, Type type)
		{
			if (!assembly.Methods.ContainsKey(type))
				assembly.InitializeMethodsForType(type);

			var methods = assembly.Methods[type];
			var items = new List<ListViewItem>();

			foreach (var method in methods)
			{
				var iconIndex = IconsStyle.GetMethodImageIndex(method);
				var methodItem = new ListViewItem(method.Name) { ImageIndex = iconIndex, Tag = method };
				items.Add(methodItem);
			}

			return items;
		}

		private static List<ListViewItem> InitializeFieldItems(DecompiledAssembly assembly, Type type)
		{
			if (!assembly.Fields.ContainsKey(type))
				assembly.InitializeFieldsForType(type);

			var fields = assembly.Fields[type];
			var items = new List<ListViewItem>();

			foreach (var field in fields)
			{
				var iconIndex = IconsStyle.GetFieldImageIndex(field);
				var fieldItem = new ListViewItem(field.Name) { ImageIndex = iconIndex, Tag = field };
				items.Add(fieldItem);
			}

			return items;
		}

		private static List<ListViewItem> InitializeEventItems(DecompiledAssembly assembly, Type type)
		{
			if (!assembly.Events.ContainsKey(type))
				assembly.InitializeEventsForType(type);

			var events = assembly.Events[type];
			var items = new List<ListViewItem>();

			foreach (var eventInfo in events)
			{
				var iconIndex = IconsStyle.GetEventImageIndex(eventInfo);
				var eventItem = new ListViewItem(eventInfo.Name) { ImageIndex = iconIndex, Tag = eventInfo };
				items.Add(eventItem);
			}

			return items;
		}

		private void CreateClicked(object sender, EventArgs e)
		{
			var fullTypeName = GetFullTypeName(treeView.SelectedNode.FullPath);
			_assemblies[_selectedAssembly.Location].Instantiate(fullTypeName);
		}

		private static string GetFullTypeName(string nodePath)
		{
			var pathParts = nodePath.Split('\\');
			var fullTypeName = string.Join(".", pathParts.Skip(1));
			return fullTypeName;
		}

		private void OpenInvokeMethodWindow(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count == 0) return;
			var selectedItem = listView.SelectedItems[0];
			var isMethod = selectedItem.Tag is MethodInfo;
			var isField = selectedItem.Tag is FieldInfo;

			if (isMethod) ShowMethodInvocationWindow();
			else if (isField) ShowFieldValueDisplayWindow(selectedItem);
		}

		private void ShowFieldValueDisplayWindow(ListViewItem selectedItem)
		{
			var assembly = _assemblies[_selectedAssembly.Location];
			if (!assembly.Instances.ContainsKey(_selectedFullTypeName)) return;

			var instance = assembly.Instances[_selectedFullTypeName];

			var fieldInfo = selectedItem.Tag as FieldInfo;
			var windowTitle = string.Format(Resources.ValueOfField, fieldInfo?.Name, _selectedFullTypeName);

			try
			{
				var fieldValue = fieldInfo?.GetValue(instance);
				MessageBox.Show(fieldValue?.ToString(), windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (NotSupportedException e)
			{
				MessageBox.Show(e.Message + e.InnerException?.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (FieldAccessException e)
			{
				MessageBox.Show(e.Message + e.InnerException?.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ShowMethodInvocationWindow()
		{
			var createdInstances = _assemblies.Values.SelectMany(a => a.Instances.Values);
			var invokeWindow = new InvokeWindow(listView.SelectedItems[0].Tag as MethodBase, createdInstances) { Owner = this };
			invokeWindow.ShowDialog();
		}

		private void IconsTo2017StyleChanged(object sender, EventArgs e)
		{
			vS2010StyleToolStripMenuItem.Checked = true;
			vS2012StyleToolStripMenuItem.Checked = false;
			listView.SmallImageList = treeView.ImageList = IconsStyle.Icons2017;
		}

		private void IconsTo2012StyleChanged(object sender, EventArgs e)
		{
			vS2010StyleToolStripMenuItem.Checked = false;
			vS2012StyleToolStripMenuItem.Checked = true;
			listView.SmallImageList = treeView.ImageList = IconsStyle.Icons2012;
		}

		private void DisplayDetailedMethodList(object sender, EventArgs e)
		{
			listToolStripMenuItem.Checked = false;
			detailsToolStripMenuItem.Checked = true;
			listView.View = View.Details;
		}

		private void DisplayMethodList(object sender, EventArgs e)
		{
			listToolStripMenuItem.Checked = true;
			detailsToolStripMenuItem.Checked = false;
			listView.View = View.List;
		}

		private void ExitClicked(object sender, EventArgs e)
		{
			Close();
		}
	}
}
