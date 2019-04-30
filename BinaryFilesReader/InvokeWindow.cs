using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BinaryFilesReader
{
    public partial class InvokeWindow : Form
    {
	    private readonly object _currentObject;

        public InvokeWindow()
        {
	        InitializeComponent();
            dataGridView.AllowUserToAddRows = false;
            var canInvoke = true;
            var form = (Browser)Application.OpenForms["Browser"];
            Type[] types = { typeof(string), typeof(int), typeof(long), typeof(float), typeof(double), typeof(bool), typeof(short) };
            var lvi = form.listView.SelectedItems[0];
            var mi = form.Methods[lvi];

            foreach (var item in mi.GetParameters())
            {
                dataGridView.Rows.Add(item.Name, item.ParameterType);
                if (!types.Contains(item.ParameterType))
                    canInvoke = false;
            }

            if (!canInvoke) return;
            foreach (var obj in form.ObjectList)
	            if (obj.GetType() == mi.ReflectedType)
	            {
		            _currentObject = obj;
		            buttonInvoke.Enabled = true;
		            dataGridView.Columns[2].Visible = true;
	            }
        }

        private void InvokeClicked(object sender, EventArgs e)
        {
	        var form = (Browser)Application.OpenForms["Browser"];
	        var lvi = form.listView.SelectedItems[0];
	        var mi = form.Methods[lvi];

	        var objectList = new List<object>();
	        foreach (DataGridViewRow row in dataGridView.Rows)
	        {
		        var type = (Type)row.Cells[1].Value;
		        var o = Convert.ChangeType(row.Cells[2].Value, type);
		        objectList.Add(o);
	        }

	        try
	        {
		        var returnValue = mi.Invoke(_currentObject, objectList.ToArray());
		        if (returnValue != null)
			        MessageBox.Show(returnValue + "", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
	        }
	        catch (Exception exception)
	        {
		        MessageBox.Show(exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
	        }
        }

		private void CellEditFinished(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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
