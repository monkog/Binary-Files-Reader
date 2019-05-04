using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BinaryFilesReader
{
	public partial class Browser : Form
	{
		public Dictionary<ListViewItem, MethodInfo> Methods = new Dictionary<ListViewItem, MethodInfo>();
		private Assembly _assembly;
		public Dictionary<string, object> CreatedInstances = new Dictionary<string, object>();

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
			if (string.IsNullOrEmpty(path)) return;

			TreeNode root;
			var tn = treeView.Nodes.Find(path, false);

			if (tn.Length != 0)
			{
				root = tn[0];
				root.Remove();
			}

			try
			{
				Assembly assembly;
				if ((assembly = Assembly.LoadFile(path)) != null)
				{
					treeView.Nodes.Add(path, path.Substring(path.LastIndexOf("\\") + 1), 6, 6);
					root = treeView.Nodes[treeView.Nodes.Count - 1];
					var thisRoot = root;

					foreach (var type in assembly.GetTypes())
					{
						var c = type.FullName.Split('.');
						for (var i = c.Length - 1; i >= 0; i--)
						{
							tn = treeView.Nodes.Find(c[i], true);
							if (tn.Length != 0)
							{
								root = tn[0];

								for (var j = i + 1; j < c.Length; j++)
								{
									root.Nodes.Add(c[j]);
									root = root.Nodes[root.Nodes.Count - 1];
									root.Name = c[j];

									var objPath = GetFullTypeName(root.FullPath);
									var tmpType = assembly.GetType(objPath);
									root.ImageIndex = GetTypeImageIndex(tmpType);
									root.SelectedImageIndex = root.ImageIndex;
								}
								root = thisRoot;
								break;
							}

							if (i == 0)
							{
								for (var j = i; j < c.Length; j++)
								{
									root.Nodes.Add(c[j]);
									root = root.Nodes[root.Nodes.Count - 1];
									root.Name = c[j];

									var objPath = GetFullTypeName(root.FullPath);
									var tmpType = assembly.GetType(objPath);
									root.ImageIndex = GetTypeImageIndex(tmpType);
									root.SelectedImageIndex = root.ImageIndex;
								}
								root = thisRoot;
								break;
							}
						}
					}
				}
				buttonCreate.Enabled = false;
			}
			catch (Exception)
			{
				MessageBox.Show("File load failed.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static int GetTypeImageIndex(Type tmpType)
		{
			if (tmpType != null)
			{
				if (tmpType.IsClass)
					return tmpType.IsSealed ? 0 : 1;
				if (tmpType.IsInterface)
					return 2;
				if (tmpType.IsNotPublic)
					return 3;
				if (tmpType.IsPublic)
					return 5;
				if (tmpType.IsNestedPrivate)
					return 4;
			}
			else
				return 6;

			throw new NotSupportedException();
		}

		private static string SelectAssemblyPath()
		{
			var openFileDialog = new OpenFileDialog { Filter = Properties.Resources.SupportedAssemblyExtensions };
			openFileDialog.ShowDialog();
			return openFileDialog.FileName;
		}

		private void ExitClicked(object sender, EventArgs e)
		{
			Close();
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

			try
			{
				_assembly = Assembly.LoadFile(root.Name);
				var objType = _assembly.GetType(fullTypeName);
				if (!objType.IsClass && !objType.IsInterface) return;

				var constructor = objType.GetConstructor(Type.EmptyTypes);
				if (constructor != null && !objType.IsAbstract)
					buttonCreate.Enabled = true;

				foreach (var method in objType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
				{
					var iconIndex = method.IsPublic ? 5 : 4;
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
			CreatedInstances[fullTypeName] = _assembly.CreateInstance(fullTypeName);
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
			var invokeWindow = new InvokeWindow(Methods[listView.SelectedItems[0]], CreatedInstances.Values) { Owner = this };
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
	}

	internal class ListViewItemComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			return String.Compare(((ListViewItem)x).Text, ((ListViewItem)y).Text);
		}
	}
}
