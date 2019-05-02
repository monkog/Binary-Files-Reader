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
	}
}
