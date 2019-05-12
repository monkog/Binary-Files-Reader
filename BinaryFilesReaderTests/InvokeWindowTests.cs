using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BinaryFilesReader;
using NUnit.Framework;

namespace BinaryFilesReaderTests
{
	[TestFixture]
	public class InvokeWindowTests
	{
		private const string Bear = "ʕ• ᴥ •ʔ";

		private MethodInfo _method;

		private InvokeWindow _unitUnderTest;

		[SetUp]
		public void Initialize()
		{
			_method = GetType().GetMethods().Single(m => m.Name == nameof(TestMethod));
			_unitUnderTest = new InvokeWindow(_method, new object[] { this });
		}

		[Test]
		public void InitializeWindow_MethodNoInstance_InvokeNotAvailable()
		{
			var unitUnderTest = new InvokeWindow(_method, new object[] { });

			InitializeWindow(unitUnderTest);

			var invokeEnabled = unitUnderTest.buttonInvoke.Enabled;

			Assert.IsFalse(invokeEnabled);
		}

		[Test]
		public void InitializeWindow_MethodWithParamsAndInstance_InvokeNotAvailable()
		{
			InitializeWindow(_unitUnderTest);

			var invokeEnabled = _unitUnderTest.buttonInvoke.Enabled;

			Assert.IsFalse(invokeEnabled);
		}

		[Test]
		public void InitializeWindow_MethodWithoutParamsAndInstance_InvokeAvailable()
		{
			var method = GetType().GetMethods().Single(m => m.Name == nameof(InitializeWindow_MethodWithoutParams_NoRows));
			var unitUnderTest = new InvokeWindow(method, new object[] { this });

			InitializeWindow(unitUnderTest);

			var invokeEnabled = unitUnderTest.buttonInvoke.Enabled;

			Assert.IsTrue(invokeEnabled);
		}

		[Test]
		public void InitializeWindow_MethodWithoutParamsNoInstance_InvokeNotAvailable()
		{
			var method = GetType().GetMethods().Single(m => m.Name == nameof(InitializeWindow_MethodWithoutParams_NoRows));
			var unitUnderTest = new InvokeWindow(method, new object[] { });

			InitializeWindow(unitUnderTest);

			var invokeEnabled = unitUnderTest.buttonInvoke.Enabled;

			Assert.IsFalse(invokeEnabled);
		}

		[Test]
		public void InitializeWindow_MethodNoInstance_ParamValueColumnInvisible()
		{
			var unitUnderTest = new InvokeWindow(_method, new object[] { });

			InitializeWindow(unitUnderTest);

			var paramColumnVisible = unitUnderTest.dataGridView.Columns[2].Visible;

			Assert.IsFalse(paramColumnVisible);
		}

		[Test]
		public void InitializeWindow_MethodWithParamsAndInstance_ParamValueColumnVisible()
		{
			InitializeWindow(_unitUnderTest);

			var paramColumnVisible = _unitUnderTest.dataGridView.Columns[2].Visible;

			Assert.IsTrue(paramColumnVisible);
		}

		[Test]
		public void InitializeWindow_MethodWithoutParamsAndInstance_ParamValueColumnVisible()
		{
			var method = GetType().GetMethods().Single(m => m.Name == nameof(InitializeWindow_MethodWithoutParams_NoRows));
			var unitUnderTest = new InvokeWindow(method, new object[] { this });

			InitializeWindow(unitUnderTest);

			var paramColumnVisible = unitUnderTest.dataGridView.Columns[2].Visible;

			Assert.IsTrue(paramColumnVisible);
		}

		[Test]
		public void InitializeWindow_MethodWithComplexParamsAndInstance_ParamValueColumnInvisible()
		{
			var method = GetType().GetMethods(BindingFlags.Static | BindingFlags.NonPublic).Single(m => m.Name == nameof(InitializeWindow));
			var unitUnderTest = new InvokeWindow(method, new object[] { this });

			InitializeWindow(unitUnderTest);

			var paramColumnVisible = unitUnderTest.dataGridView.Columns[2].Visible;

			Assert.IsFalse(paramColumnVisible);
		}

		[Test]
		public void InitializeWindow_MethodWithoutParamsNoInstance_ParamValueColumnInvisible()
		{
			var method = GetType().GetMethods().Single(m => m.Name == nameof(InitializeWindow_MethodWithoutParams_NoRows));
			var unitUnderTest = new InvokeWindow(method, new object[] { });

			InitializeWindow(unitUnderTest);

			var paramColumnVisible = unitUnderTest.dataGridView.Columns[2].Visible;

			Assert.IsFalse(paramColumnVisible);
		}

		[Test]
		public void InitializeWindow_MethodWithParams_RowForEachParam()
		{
			InitializeWindow(_unitUnderTest);

			var rowCount = _unitUnderTest.dataGridView.RowCount;

			Assert.AreEqual(2, rowCount);
		}

		[Test]
		public void InitializeWindow_MethodWithParams_NameAndTypeColumns()
		{
			InitializeWindow(_unitUnderTest);

			var firstParamData = _unitUnderTest.dataGridView.Rows[0];
			var secondParamData = _unitUnderTest.dataGridView.Rows[1];

			Assert.AreEqual("x", firstParamData.Cells[0].Value);
			Assert.AreEqual(typeof(int), firstParamData.Cells[1].Value);
			Assert.AreEqual("y", secondParamData.Cells[0].Value);
			Assert.AreEqual(typeof(int), secondParamData.Cells[1].Value);
		}

		[Test]
		public void InitializeWindow_MethodWithoutParams_NoRows()
		{
			var method = GetType().GetMethods().Single(m => m.Name == nameof(InitializeWindow_MethodWithoutParams_NoRows));
			var unitUnderTest = new InvokeWindow(method, new object[] { });

			InitializeWindow(unitUnderTest);

			var rowCount = unitUnderTest.dataGridView.RowCount;

			Assert.AreEqual(0, rowCount);
		}

		[Test]
		public void CellEditFinished_AllValuesProvided_InvokeEnabled()
		{
			InitializeWindow(_unitUnderTest);

			_unitUnderTest.dataGridView.Rows[0].Cells[2].Value = 2;
			_unitUnderTest.dataGridView.Rows[1].Cells[2].Value = 2;

			CellEditFinished(1);

			Assert.IsTrue(_unitUnderTest.buttonInvoke.Enabled);
		}

		[Test]
		public void CellEditFinished_NotAllValuesProvided_InvokeDisabled()
		{
			InitializeWindow(_unitUnderTest);

			_unitUnderTest.dataGridView.Rows[1].Cells[2].Value = 2;

			CellEditFinished(1);

			Assert.IsFalse(_unitUnderTest.buttonInvoke.Enabled);
		}

		[Test]
		public void CellEditFinished_InvalidValueProvided_InvokeDisabled()
		{
			InitializeWindow(_unitUnderTest);

			_unitUnderTest.dataGridView.Rows[0].Cells[2].Value = Bear;
			_unitUnderTest.dataGridView.Rows[1].Cells[2].Value = 2;

			CellEditFinished(0);

			Assert.IsFalse(_unitUnderTest.buttonInvoke.Enabled);
		}

		[Test]
		public void CellEditFinished_InvalidValueProvided_CellNotReadOnly()
		{
			InitializeWindow(_unitUnderTest);

			_unitUnderTest.dataGridView.Rows[0].Cells[2].Value = Bear;

			CellEditFinished(0);
			var cell = _unitUnderTest.dataGridView.Rows[0].Cells[2];

			Assert.IsFalse(cell.ReadOnly);
		}

		[Test]
		public void CellEditFinished_InvalidValueProvided_OtherCellsReadOnly()
		{
			InitializeWindow(_unitUnderTest);

			_unitUnderTest.dataGridView.Rows[0].Cells[2].Value = Bear;
			_unitUnderTest.dataGridView.Rows[1].Cells[2].Value = 2;

			CellEditFinished(0);
			var otherCell = _unitUnderTest.dataGridView.Rows[1].Cells[2];

			Assert.IsTrue(otherCell.ReadOnly);
		}

		[Test]
		public void CellEditFinished_InvalidValueProvided_RowErrorVisible()
		{
			InitializeWindow(_unitUnderTest);

			_unitUnderTest.dataGridView.Rows[0].Cells[2].Value = Bear;

			CellEditFinished(0);
			var cell = _unitUnderTest.dataGridView.Rows[0];

			Assert.IsFalse(string.IsNullOrEmpty(cell.ErrorText) || string.IsNullOrWhiteSpace(cell.ErrorText));
		}

		public int TestMethod(int x, int y)
		{
			return x + y;
		}

		private static void InitializeWindow(InvokeWindow control)
		{
			var initializeMethod = control.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Single(m => m.Name == "Initialized");
			initializeMethod.Invoke(control, new object[] { null, EventArgs.Empty });
		}

		private void CellEditFinished(int rowIndex)
		{
			var initializeMethod = _unitUnderTest.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Single(m => m.Name == "CellEditFinished");
			var eventArgs = new DataGridViewCellEventArgs(2, rowIndex);
			initializeMethod.Invoke(_unitUnderTest, new object[] { null, eventArgs });
		}
	}
}
