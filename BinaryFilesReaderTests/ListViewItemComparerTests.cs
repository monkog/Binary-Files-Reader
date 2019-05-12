using System;
using NUnit.Framework;
using System.Windows.Forms;
using BinaryFilesReader;

namespace BinaryFilesReaderTests
{
	[TestFixture]
	public class ListViewItemComparerTests
	{
		private const string Bear = "ʕ• ᴥ •ʔ";
		private ListViewItemComparer _unitUnderTest;

		[SetUp]
		public void Initialize()
		{
			_unitUnderTest = new ListViewItemComparer();
		}

		[Test]
		public void Compare_SameListViewItems_0()
		{
			var item = new ListViewItem { Text = Bear };

			var result = _unitUnderTest.Compare(item, item);

			Assert.AreEqual(0, result);
		}

		[Test]
		public void Compare_SmallerItemToGreater_NegativeValue()
		{
			var item1 = new ListViewItem { Text = Bear };
			var item2 = new ListViewItem { Text = Bear + Bear };

			var result = _unitUnderTest.Compare(item1, item2);

			Assert.Less(result, 0);
		}

		[Test]
		public void Compare_GreaterItemToSmaller_PositiveValue()
		{
			var item1 = new ListViewItem { Text = Bear };
			var item2 = new ListViewItem { Text = Bear + Bear };

			var result = _unitUnderTest.Compare(item2, item1);

			Assert.Greater(result, 0);
		}

		[Test]
		public void Compare_ListViewItemToOtherObject_Exception()
		{
			var item1 = new ListViewItemComparerTests();
			var item2 = new ListViewItem { Text = Bear };

			void Compare()
			{
				var _ = _unitUnderTest.Compare(item1, item2);
			}

			Assert.Throws<ArgumentException>(Compare);
		}

		[Test]
		public void Compare_OtherObjectToListViewItem_Exception()
		{
			var item1 = new ListViewItem { Text = Bear };
			var item2 = new ListViewItemComparerTests();

			void Compare()
			{
				var _ = _unitUnderTest.Compare(item1, item2);
			}

			Assert.Throws<ArgumentException>(Compare);
		}

		[Test]
		public void Compare_OtherObjects_Exception()
		{
			var item1 = new ListViewItemComparerTests();
			var item2 = new ListViewItemComparerTests();

			void Compare()
			{
				var _ = _unitUnderTest.Compare(item1, item2);
			}

			Assert.Throws<ArgumentException>(Compare);
		}
	}
}
