using System;
using System.Reflection;
using System.Windows.Forms;

namespace BinaryFilesReader
{
	public static class IconsStyle
	{
		/// <summary>
		/// Gets the image list of Visual Studio 2010 style.
		/// </summary>
		public static ImageList Icons2010 { get; }

		/// <summary>
		/// Gets the image list of Visual Studio 2012 style.
		/// </summary>
		public static ImageList Icons2012 { get; }

		static IconsStyle()
		{
			Icons2010 = new ImageList();
			Icons2010.Images.Add(Properties.Resources.class_sealedvs10);
			Icons2010.Images.Add(Properties.Resources.classvs10);
			Icons2010.Images.Add(Properties.Resources.interfacevs10);
			Icons2010.Images.Add(Properties.Resources.method_privatevs10);
			Icons2010.Images.Add(Properties.Resources.method_protectedvs10);
			Icons2010.Images.Add(Properties.Resources.method_publicvs10);
			Icons2010.Images.Add(Properties.Resources.namespacevs10);

			Icons2012 = new ImageList();
			Icons2012.Images.Add(Properties.Resources.class_sealedvs11);
			Icons2012.Images.Add(Properties.Resources.classvs11);
			Icons2012.Images.Add(Properties.Resources.interfacevs11);
			Icons2012.Images.Add(Properties.Resources.method_privatevs11);
			Icons2012.Images.Add(Properties.Resources.method_protectedvs11);
			Icons2012.Images.Add(Properties.Resources.method_publicvs11);
			Icons2012.Images.Add(Properties.Resources.namespacevs11);
		}

		/// <summary>
		/// Gets the image index corresponding to the provided type.
		/// </summary>
		/// <param name="type">Object type.</param>
		/// <returns>Index of the icon.</returns>
		public static int GetTypeImageIndex(Type type)
		{
			if (type != null)
			{
				if (type.IsClass)
					return type.IsSealed ? 0 : 1;
				if (type.IsInterface)
					return 2;
			}
			else
				return 6;

			throw new NotSupportedException(Properties.Resources.ObjectIconNotSupported);
		}

		/// <summary>
		/// Gets the image index corresponding to the provided method info.
		/// </summary>
		/// <param name="method">Method info.</param>
		/// <returns>Index of the icon.</returns>
		public static int GetMethodImageIndex(MethodInfo method)
		{
			if (method.IsPrivate)
				return 3;

			return method.IsPublic ? 5 : 4;
		}
	}
}
