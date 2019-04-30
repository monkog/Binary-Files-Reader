using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BinaryFilesReader
{
    public partial class InvokeWindow : Form
    {
        bool canInvoke;
        object currentObject = null;

        public InvokeWindow()
        {
            InitializeComponent();
            dataGridView.AllowUserToAddRows = false;
            canInvoke = true;
            Browser form = (Browser)Application.OpenForms["Browser"];
            Type[] types = new Type[] { typeof(string), typeof(int), typeof(long), typeof(float), typeof(double), typeof(bool), typeof(short) };
            ListViewItem lvi = form.listView.SelectedItems[0];
            MethodInfo mi = form.methods[lvi];

            foreach (ParameterInfo item in mi.GetParameters())
            {
                dataGridView.Rows.Add(item.Name, item.ParameterType);
                if (!types.Contains(item.ParameterType))
                    canInvoke = false;
            }

            if (canInvoke)
                foreach (object obj in form.objectList)
                    if (obj.GetType() == mi.ReflectedType)
                    {
                        currentObject = obj;
                        buttonInvoke.Enabled = true;
                        dataGridView.Columns[2].Visible = true;
                    }
        }

        private void buttonInvoke_Click(object sender, EventArgs e)
        {
            object returnValue;
            Browser form = (Browser)Application.OpenForms["Browser"];
            ListViewItem lvi = form.listView.SelectedItems[0];
            MethodInfo mi = form.methods[lvi];

            List<object> objectList = new List<object>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                Type type;
                object o;
                type = (Type)row.Cells[1].Value;
                o = Convert.ChangeType(row.Cells[2].Value, type);
                objectList.Add(o);
            }
            

            try
            {
                returnValue = mi.Invoke(currentObject, objectList.ToArray());
                if (returnValue != null)
                    MessageBox.Show(returnValue + "", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Type type;
                object o;
                type = (Type)dataGridView.Rows[e.RowIndex].Cells[1].Value;
                o = Convert.ChangeType(dataGridView.Rows[e.RowIndex].Cells[2].Value, type);
                buttonInvoke.Enabled = true;
                dataGridView.Columns[2].ReadOnly = false;
                dataGridView.ShowRowErrors = false;
            }
            catch (Exception)
            {
                buttonInvoke.Enabled = false;
                dataGridView.Columns[2].ReadOnly = true;
                dataGridView.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                dataGridView.ShowRowErrors = true;
                dataGridView.CancelEdit();
                dataGridView.Rows[e.RowIndex].ErrorText = "The entered value has incorrect format";
            }
        }
    }
}
