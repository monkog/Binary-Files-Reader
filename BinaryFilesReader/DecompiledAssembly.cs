using System;
using System.Collections.Generic;
using System.Reflection;

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

		/// <summary>
		/// Gets the collection of available methods for the type.
		/// </summary>
		public Dictionary<Type, MethodInfo[]> Methods { get; }

		/// <summary>
		/// Gets the collection of created instances of this assembly types.
		/// </summary>
		public Dictionary<string, object> Instances { get; }

		public DecompiledAssembly(string path)
		{
			Assembly = Assembly.LoadFile(path);
			Types =new Dictionary<string, Type>();
			Instances = new Dictionary<string, object>();
			Methods = new Dictionary<Type, MethodInfo[]>();

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
		
		/// <summary>
		/// Initializes the available method collection for the provided type.
		/// </summary>
		/// <param name="type">Type to initialize the method collection for.</param>
		public void InitializeMethodsForType(Type type)
		{
			if(!Types.ContainsValue(type))
				throw new ArgumentException(Properties.Resources.TypeNotDefinedInAssembly);

			Methods[type] = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
		}
	}
}
