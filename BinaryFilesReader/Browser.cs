using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace BinaryFilesReader
{
	public partial class Browser : Form
	{
		public Dictionary<ListViewItem, MethodInfo> Methods = new Dictionary<ListViewItem, MethodInfo>();
		private Assembly _assembly;
		private string _objPath;
		private readonly ImageList _icons2010 = new ImageList();
		private readonly ImageList _icons2012 = new ImageList();
		public Dictionary<string, object> CreatedInstances = new Dictionary<string, object>();

		public Browser()
		{
			InitializeComponent();
			_icons2010.Images.Add(Properties.Resources.class_sealedvs10);
			_icons2010.Images.Add(Properties.Resources.classvs10);
			_icons2010.Images.Add(Properties.Resources.interfacevs10);
			_icons2010.Images.Add(Properties.Resources.method_privatevs10);
			_icons2010.Images.Add(Properties.Resources.method_protectedvs10);
			_icons2010.Images.Add(Properties.Resources.method_publicvs10);
			_icons2010.Images.Add(Properties.Resources.namespacevs10);

			_icons2012.Images.Add(Properties.Resources.class_sealedvs11);
			_icons2012.Images.Add(Properties.Resources.classvs11);
			_icons2012.Images.Add(Properties.Resources.interfacevs11);
			_icons2012.Images.Add(Properties.Resources.method_privatevs11);
			_icons2012.Images.Add(Properties.Resources.method_protectedvs11);
			_icons2012.Images.Add(Properties.Resources.method_publicvs11);
			_icons2012.Images.Add(Properties.Resources.namespacevs11);
			treeView.ImageList = _icons2010;
			listView.SmallImageList = _icons2010;

			listView.ListViewItemSorter = new ListViewItemComparer();
		}

		private void LoadFileClicked(object sender, EventArgs e)
		{
			var path = SelectAssemblyPath();

			if (string.IsNullOrEmpty(path)) return;

			Assembly assembly = null;
			TreeNode root;

			var tn = treeView.Nodes.Find(path, false);

			if (tn.Length != 0)
			{
				root = tn[0];
				root.Remove();
			}

			try
			{
				if ((assembly = Assembly.LoadFile(path)) != null)
				{
					treeView.Nodes.Add(path.Substring(path.LastIndexOf("\\") + 1));
					root = treeView.Nodes[treeView.Nodes.Count - 1];
					root.Name = path;
					root.ImageIndex = 6;
					root.SelectedImageIndex = root.ImageIndex;
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

									var objPath = root.FullPath.Substring(root.FullPath.IndexOf('\\') + 1);
									objPath = objPath.Replace('\\', '.');

									var tmpType = assembly.GetType(objPath);
									if (tmpType != null)
									{
										if (tmpType.IsClass)
											root.ImageIndex = tmpType.IsSealed ? 0 : 1;
										else if (tmpType.IsInterface)
											root.ImageIndex = 2;
										else if (tmpType.IsNotPublic)
											root.ImageIndex = 3;
										else if (tmpType.IsPublic)
											root.ImageIndex = 5;
										else if (tmpType.IsNestedPrivate)
											root.ImageIndex = 4;
									}
									else
										root.ImageIndex = 6;
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

									var objPath = root.FullPath.Substring(root.FullPath.IndexOf('\\') + 1);
									objPath = objPath.Replace('\\', '.');

									var tmpType = assembly.GetType(objPath);
									if (tmpType != null)
									{
										if (tmpType.IsClass)
											if (tmpType.IsSealed)
												root.ImageIndex = 0;
											else
												root.ImageIndex = 1;
										else if (tmpType.IsInterface)
											root.ImageIndex = 2;
										else if (tmpType.IsNotPublic)
											root.ImageIndex = 3;
										else if (tmpType.IsPublic)
											root.ImageIndex = 5;
										else if (tmpType.IsNestedPrivate)
											root.ImageIndex = 4;
									}
									else
										root.ImageIndex = 6;
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
			string path = e.Node.FullPath, rootName;
			TreeNode root = null;
			try
			{
				rootName = path.Substring(0, path.IndexOf('\\'));
			}
			catch (Exception)
			{
				return;
			}

			foreach (TreeNode treeNode in treeView.Nodes)
				if (treeNode.Text == rootName)
				{
					root = treeNode;
					break;
				}

			_assembly = Assembly.LoadFile(root.Name);
			_objPath = path.Substring(path.IndexOf('\\') + 1);
			_objPath = _objPath.Replace('\\', '.');

			buttonCreate.Enabled = false;

			try
			{
				listView.Items.Clear();
				var objType = _assembly.GetType(_objPath);
				var ci = objType.GetConstructor(Type.EmptyTypes);
				if (!objType.IsClass && !objType.IsInterface) return;
				if (ci != null && objType.IsAbstract == false)
					buttonCreate.Enabled = true;
				foreach (var method in objType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
					if (method.IsPublic)
					{
						var lvi = new ListViewItem(method.Name)
						{
							ImageIndex = 5,
							StateImageIndex = 5,
							Name = method.DeclaringType?.FullName
						};
						listView.Items.Add(lvi);
						Methods.Add(lvi, method);
					}
					else
					{
						var lvi = new ListViewItem(method.Name) { ImageIndex = 4, StateImageIndex = 4 };
						listView.Items.Add(lvi);
						Methods.Add(lvi, method);
					}
				listView.Sort();
			}
			catch (Exception)
			{
				listView.Items.Clear();
			}
		}

		private void CreateClicked(object sender, EventArgs e)
		{
			CreatedInstances[_objPath] = _assembly.CreateInstance(_objPath);
		}

		private void IconsTo2010StyleChanged(object sender, EventArgs e)
		{
			vS2010StyleToolStripMenuItem.Checked = true;
			vS2012StyleToolStripMenuItem.Checked = false;
			listView.SmallImageList = treeView.ImageList = _icons2010;
		}

		private void IconsTo2012StyleChanged(object sender, EventArgs e)
		{
			vS2010StyleToolStripMenuItem.Checked = false;
			vS2012StyleToolStripMenuItem.Checked = true;
			listView.SmallImageList = treeView.ImageList = _icons2012;
		}

		private void OpenInvokeMethodWindow(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count == 0) return;
			var invokeWindow = new InvokeWindow(Methods[listView.SelectedItems[0]], CreatedInstances.Values) { Owner = this };
			invokeWindow.ShowDialog();
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
