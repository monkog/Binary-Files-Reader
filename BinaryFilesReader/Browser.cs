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
		public Dictionary<string, DecompiledAssembly> Assemblies = new Dictionary<string, DecompiledAssembly>();
		public Dictionary<ListViewItem, MethodInfo> Methods = new Dictionary<ListViewItem, MethodInfo>();
		private Assembly _assembly;

		public Browser()
		{
			InitializeComponent();

			treeView.ImageList = IconsStyle.Icons2010;
			listView.SmallImageList = IconsStyle.Icons2010;
			listView.ListViewItemSorter = new ListViewItemComparer();
		}

		private void LoadFileClicked(object sender, EventArgs e)
		{
			var path = SelectAssemblyPath();
			if (string.IsNullOrEmpty(path) || Assemblies.ContainsKey(path)) return;

			if (!TryOpenAssembly(path, out var assembly)) return;
			Assemblies[path] = assembly;
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
			var root = treeView.Nodes.Add(path, path.Substring(path.LastIndexOf('\\') + 1), 6, 6);

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
			var openFileDialog = new OpenFileDialog { Filter = Properties.Resources.SupportedAssemblyExtensions };
			openFileDialog.ShowDialog();
			return openFileDialog.FileName;
		}

		private void AssemblyObjectSelected(object sender, TreeViewEventArgs e)
		{
			var pathParts = e.Node.FullPath.Split('\\');
			if (pathParts.Length == 1) return;

			TreeNode root = null;
			foreach (TreeNode treeNode in treeView.Nodes)
				if (treeNode.Text == pathParts[0])
				{
					root = treeNode;
					break;
				}

			var fullTypeName = GetFullTypeName(e.Node.FullPath);
			buttonCreate.Enabled = false;
			Methods.Clear();
			listView.Items.Clear();

			var assembly = Assemblies[root.Name];
			_assembly = assembly.Assembly;

			if (!assembly.Types.ContainsKey(fullTypeName)) return;

			var type = assembly.Types[fullTypeName];
			if (!type.IsClass && !type.IsInterface) return;

			try
			{
				var constructor = type.GetConstructor(Type.EmptyTypes);
				if (constructor != null && !type.IsAbstract)
					buttonCreate.Enabled = true;

				foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
				{
					var iconIndex = IconsStyle.GetMethodImageIndex(method);
					var methodItem = new ListViewItem(method.Name) { ImageIndex = iconIndex, StateImageIndex = iconIndex };
					Methods.Add(methodItem, method);
				}
			}
			catch (Exception)
			{
				// temporarily ignore all issues
			}

			listView.Items.AddRange(Methods.Keys.ToArray());
			listView.Sort();
		}

		private void CreateClicked(object sender, EventArgs e)
		{
			var fullTypeName = GetFullTypeName(treeView.SelectedNode.FullPath);
			Assemblies[_assembly.Location].Instantiate(fullTypeName);
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
			var createdInstances = Assemblies.Values.SelectMany(a => a.Instances.Values);
			var invokeWindow = new InvokeWindow(Methods[listView.SelectedItems[0]], createdInstances) { Owner = this };
			invokeWindow.ShowDialog();
		}

		private void IconsTo2010StyleChanged(object sender, EventArgs e)
		{
			vS2010StyleToolStripMenuItem.Checked = true;
			vS2012StyleToolStripMenuItem.Checked = false;
			listView.SmallImageList = treeView.ImageList = IconsStyle.Icons2010;
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
