using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BinaryFilesReader.Extensions;

namespace BinaryFilesReader
{
	public partial class InvokeWindow : Form
	{
		private readonly MethodBase _method;
		private readonly IEnumerable<object> _assemblyTypeInstances;

		private object _currentInstance;

		/// <summary>
		/// Gets the value indicating whether all values necessary for invoking the method were provided.
		/// </summary>
		public bool AllValuesProvided { get { return dataGridView.Rows.Cast<DataGridViewRow>().All(row => row.Cells[2].Value != null); } }

		public InvokeWindow(MethodBase method, IEnumerable<object> assemblyTypeInstances)
		{
			InitializeComponent();

			_method = method;
			_assemblyTypeInstances = assemblyTypeInstances;
		}

		private void Initialized(object sender, EventArgs e)
		{
			var parameters = _method.GetParameters();

			foreach (var parameter in parameters)
			{
				dataGridView.Rows.Add(parameter.Name, parameter.ParameterType);
			}

			var allParametersAreSimpleType = parameters.All(p => p.ParameterType.IsSimpleType());
			_currentInstance = _assemblyTypeInstances.SingleOrDefault(i => i.GetType() == _method.ReflectedType);

			if (!allParametersAreSimpleType || _currentInstance == null) return;

			buttonInvoke.Enabled = !parameters.Any();
			dataGridView.Columns[2].Visible = true;
		}

		private void CellEditFinished(object sender, DataGridViewCellEventArgs e)
		{
			var typeName = (Type)dataGridView.Rows[e.RowIndex].Cells[1].Value;
			var value = dataGridView.Rows[e.RowIndex].Cells[2].Value;
			var isValueValid = typeName.ConvertObject(value, out var error) != null;

			buttonInvoke.Enabled = isValueValid && AllValuesProvided;
			dataGridView.Columns[2].ReadOnly = !isValueValid;
			dataGridView.Rows[e.RowIndex].Cells[2].ReadOnly = false;
			dataGridView.ShowRowErrors = !isValueValid;
			dataGridView.Rows[e.RowIndex].ErrorText = error;

			if (!isValueValid) dataGridView.CancelEdit();
		}

		private void InvokeClicked(object sender, EventArgs e)
		{
			var parameters = new List<object>();
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				var type = (Type)row.Cells[1].Value;
				var value = row.Cells[2].Value;
				parameters.Add(type.ConvertObject(value, out _));
			}

			InvokeMethod(parameters);
		}

		private void InvokeMethod(List<object> parameters)
		{
			var invokedMethod = string.Format(Properties.Resources.InvocationResultWindowTitle, _method.Name);
			try
			{
				var returnValue = _method.Invoke(_currentInstance, parameters.ToArray());
				if (returnValue != null)
					MessageBox.Show(returnValue.ToString(), invokedMethod, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message + exception.InnerException?.Message, invokedMethod, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
