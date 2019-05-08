using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace BinaryFilesReader
{
	public class DecompiledAssembly
	{
		/// <summary>
		/// Gets the loaded assembly.
		/// </summary>
		public Assembly Assembly { get; }

		/// <summary>
		/// Gets the collection of available types.
		/// </summary>
		public Dictionary<string, Type> Types { get; }

		public Dictionary<ListViewItem, MethodInfo> Methods { get; }

		/// <summary>
		/// Gets the collection of created instances of this assembly types.
		/// </summary>
		public Dictionary<string, object> Instances { get; }

		public DecompiledAssembly(string path)
		{
			Assembly = Assembly.LoadFile(path);
			Types =new Dictionary<string, Type>();
			Instances = new Dictionary<string, object>();

			foreach (var type in Assembly.GetTypes())
			{
				Types.Add(type.FullName, type);
			}
		}

		/// <summary>
		/// Instantiates an object of provided type name.
		/// </summary>
		/// <param name="typeName">Name of the object to instantiate.</param>
		public void Instantiate(string typeName)
		{
			if(!Types.ContainsKey(typeName))
				throw new ArgumentException(Properties.Resources.TypeNotDefinedInAssembly);

			Instances[typeName] = Assembly.CreateInstance(typeName);
		}
	}
}
