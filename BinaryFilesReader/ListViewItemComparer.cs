using System;
using System.Collections;
using System.Windows.Forms;

namespace BinaryFilesReader
{
	public class ListViewItemComparer : IComparer
	{
		/// <inheritdoc/>
		public int Compare(object item1, object item2)
		{
			if (!(item1 is ListViewItem && item2 is ListViewItem))
				throw new ArgumentException(Properties.Resources.ArgumentShouldBeOfTypeListViewItem);

			return string.Compare(((ListViewItem)item1).Text, ((ListViewItem)item2).Text, StringComparison.InvariantCulture);
		}
	}
}