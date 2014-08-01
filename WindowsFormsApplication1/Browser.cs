using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class Browser : Form
    {
        public Dictionary<ListViewItem, MethodInfo> methods;
        Assembly asm = null;
        string objPath = null;
        ImageList icons2010 = new ImageList();
        ImageList icons2012 = new ImageList();
        public List<object> objectList = new List<object>();

        public Browser()
        {
            InitializeComponent();
            icons2010.Images.Add(Properties.Resources.class_sealedvs10);
            icons2010.Images.Add(Properties.Resources.classvs10);
            icons2010.Images.Add(Properties.Resources.interfacevs10);
            icons2010.Images.Add(Properties.Resources.method_privatevs10);
            icons2010.Images.Add(Properties.Resources.method_protectedvs10);
            icons2010.Images.Add(Properties.Resources.method_publicvs10);
            icons2010.Images.Add(Properties.Resources.namespacevs10);

            icons2012.Images.Add(Properties.Resources.class_sealedvs11);
            icons2012.Images.Add(Properties.Resources.classvs11);
            icons2012.Images.Add(Properties.Resources.interfacevs11);
            icons2012.Images.Add(Properties.Resources.method_privatevs11);
            icons2012.Images.Add(Properties.Resources.method_protectedvs11);
            icons2012.Images.Add(Properties.Resources.method_publicvs11);
            icons2012.Images.Add(Properties.Resources.namespacevs11);
            treeView.ImageList = icons2010;
            listView.SmallImageList = icons2010;

            listView.DoubleClick += listView_DoubleClick;
            listView.Sorting = SortOrder.None;
            listView.ListViewItemSorter = new ListViewItemComparer();

            listView.ContextMenuStrip = contextMenuStrip;
            listView.ContextMenuStrip.Items[0].Click += ListClick;
            listView.ContextMenuStrip.Items[1].Click += DetailsClick;

            methods = new Dictionary<ListViewItem, MethodInfo>();
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".NET binaries (*.dll; *exe)|*.dll;*.exe";
            ofd.ShowDialog();
            this.treeView.Enabled = true;
            this.treeView.Visible = true;

            string path = ofd.FileName; string bla = ofd.ToString();
            Assembly assembly = null;
            TreeNode[] tn = null;
            TreeNode root = new TreeNode();

            tn = treeView.Nodes.Find(path, false);

            if (tn.Length != 0)
            {
                root = tn[0];
                root.Remove();
                root = new TreeNode();
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
                    TreeNode thisRoot = root;

                    foreach (Type type in assembly.GetTypes())
                    {
                        type.GetMethods().ToList();

                        string[] c = type.FullName.Split('.');
                        for (int i = c.Length - 1; i >= 0; i--)
                        {
                            tn = treeView.Nodes.Find(c[i], true);
                            if (tn.Length != 0)
                            {
                                root = tn[0];

                                for (int j = i + 1; j < c.Length; j++)
                                {
                                    root.Nodes.Add(c[j]);
                                    root = root.Nodes[root.Nodes.Count - 1];
                                    root.Name = c[j];

                                    string objPath = root.FullPath.Substring(root.FullPath.IndexOf('\\') + 1);
                                    objPath = objPath.Replace('\\', '.');

                                    Type tmpType = assembly.GetType(objPath);
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
                            else if (i == 0)
                            {
                                for (int j = i; j < c.Length; j++)
                                {
                                    root.Nodes.Add(c[j]);
                                    root = root.Nodes[root.Nodes.Count - 1];
                                    root.Name = c[j];

                                    string objPath = root.FullPath.Substring(root.FullPath.IndexOf('\\') + 1);
                                    objPath = objPath.Replace('\\', '.');

                                    Type tmpType = assembly.GetType(objPath);
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
                if (assembly != null)
                    MessageBox.Show("File load failed.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
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

            asm = Assembly.LoadFile(root.Name);
            objPath = path.Substring(path.IndexOf('\\') + 1);
            objPath = objPath.Replace('\\', '.');

            buttonCreate.Enabled = false;

            try
            {
                listView.Items.Clear();
                Type objType = asm.GetType(objPath);
                ConstructorInfo ci = objType.GetConstructor(Type.EmptyTypes);
                if (objType.IsClass || objType.IsInterface)
                {
                    if (ci != null && objType.IsAbstract == false)
                        buttonCreate.Enabled = true;
                    foreach (var method in objType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                        if (method.IsPublic)
                        {
                            ListViewItem lvi = new ListViewItem(method.Name);
                            lvi.ImageIndex = 5;
                            lvi.StateImageIndex = 5;
                            lvi.Name = method.DeclaringType.FullName;
                            listView.Items.Add(lvi);
                            methods.Add(lvi, method);
                        }
                        else
                        {
                            ListViewItem lvi = new ListViewItem(method.Name);
                            lvi.ImageIndex = 4;
                            lvi.StateImageIndex = 4;
                            listView.Items.Add(lvi);
                            methods.Add(lvi, method);
                        }
                    listView.Sort();
                }
            }
            catch (Exception)
            {
                listView.Items.Clear();
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (objectList.Count != 0)
                foreach (object obj in objectList)
                    if (obj.GetType() == asm.GetType(objPath))
                    {
                        objectList.Remove(obj);
                        break;
                    }

            objectList.Add(asm.CreateInstance(objPath));
        }

        private void vS2010StyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vS2010StyleToolStripMenuItem.Checked = true;
            vS2012StyleToolStripMenuItem.Checked = false;
            listView.SmallImageList = treeView.ImageList = icons2010;
        }

        private void vS2012StyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vS2010StyleToolStripMenuItem.Checked = false;
            vS2012StyleToolStripMenuItem.Checked = true;
            listView.SmallImageList = treeView.ImageList = icons2012;
        }

        void listView_DoubleClick(object sender, EventArgs e)
        {
                if (listView.SelectedItems.Count > 0)
                {
                    InvokeWindow invokeWindow = new InvokeWindow();
                    invokeWindow.Owner = this;
                    invokeWindow.ShowDialog();
                }
        }

        private void DetailsClick(object sender, EventArgs e)
        {
            listToolStripMenuItem.Checked = false;
            detailsToolStripMenuItem.Checked = true;
            listView.View = View.Details;
        }

        private void ListClick(object sender, EventArgs e)
        {
            listToolStripMenuItem.Checked = true;
            detailsToolStripMenuItem.Checked = false;
            listView.View = View.List;
        }
    }

    class ListViewItemComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).Text, ((ListViewItem)y).Text);
        }
    }
}
