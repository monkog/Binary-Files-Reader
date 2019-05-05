using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BinaryFilesReader
{
	public class DecompiledAssembly
	{
		public Assembly Assembly { get; }

		public IEnumerable<string> Types { get; }

		public Dictionary<ListViewItem, MethodInfo> Methods { get; }

		public DecompiledAssembly(string path)
		{
			Assembly = Assembly.LoadFile(path);
			Types = Assembly.GetTypes().Select(t => t.FullName);
		}
	}
}
