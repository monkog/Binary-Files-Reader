using System;
using System.Reflection;
using System.Windows.Forms;

namespace BinaryFilesReader
{
	public static class IconsStyle
	{
		/// <summary>
		/// Gets the image list of Visual Studio 2017 style.
		/// </summary>
		public static ImageList Icons2017 { get; }

		/// <summary>
		/// Gets the image list of Visual Studio 2012 style.
		/// </summary>
		public static ImageList Icons2012 { get; }

		static IconsStyle()
		{
			Icons2017 = new ImageList();
			Icons2017.Images.Add(Properties.Resources.Library_16x);
			Icons2017.Images.Add(Properties.Resources.Class_16x);
			Icons2017.Images.Add(Properties.Resources.ClassPrivate_16x);
			Icons2017.Images.Add(Properties.Resources.ClassProtected_16x);
			Icons2017.Images.Add(Properties.Resources.ClassSealed_16x);
			Icons2017.Images.Add(Properties.Resources.Interface_16x);
			Icons2017.Images.Add(Properties.Resources.InterfacePrivate_16x);
			Icons2017.Images.Add(Properties.Resources.InterfaceProtect_16x);
			Icons2017.Images.Add(Properties.Resources.Namespace_16x);
			Icons2017.Images.Add(Properties.Resources.Method_16x);
			Icons2017.Images.Add(Properties.Resources.MethodPrivate_16x);
			Icons2017.Images.Add(Properties.Resources.MethodProtect_16x);
			Icons2017.Images.Add(Properties.Resources.Field_16x);
			Icons2017.Images.Add(Properties.Resources.FieldPrivate_16x);
			Icons2017.Images.Add(Properties.Resources.FieldProtect_16x);
			Icons2017.Images.Add(Properties.Resources.Event_16x);

			Icons2012 = new ImageList();
			Icons2012.Images.Add(Properties.Resources.Library_6213);
			Icons2012.Images.Add(Properties.Resources.ClassIcon);
			Icons2012.Images.Add(Properties.Resources.Class_Private_493);
			Icons2012.Images.Add(Properties.Resources.Class_Protected_492);
			Icons2012.Images.Add(Properties.Resources.Class_Sealed_490);
			Icons2012.Images.Add(Properties.Resources.Interface_612);
			Icons2012.Images.Add(Properties.Resources.Interface_Private_616);
			Icons2012.Images.Add(Properties.Resources.Interface_Protected_615);
			Icons2012.Images.Add(Properties.Resources.Namespace_654);
			Icons2012.Images.Add(Properties.Resources.Method_636);
			Icons2012.Images.Add(Properties.Resources.Method_Private_640);
			Icons2012.Images.Add(Properties.Resources.Method_Protected_639);
			Icons2012.Images.Add(Properties.Resources.FieldIcon);
			Icons2012.Images.Add(Properties.Resources.Field_Private_545);
			Icons2012.Images.Add(Properties.Resources.Field_Protected_544);
			Icons2012.Images.Add(Properties.Resources.Event_594);
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
				{
					if (type.IsSealed)
						return 4;
					if (type.IsPublic)
						return 1;
					if (type.IsNestedFamily)
						return 3;
					return 2;
				}
				if (type.IsInterface)
				{
					if (type.IsPublic)
						return 5;
					if (type.IsNestedFamily)
						return 7;
					return 6;
				}
			}
			else
				return 8;

			throw new NotSupportedException(Properties.Resources.ObjectIconNotSupported);
		}

		/// <summary>
		/// Gets the image index corresponding to the provided method info.
		/// </summary>
		/// <param name="method">Method info.</param>
		/// <returns>Index of the icon.</returns>
		public static int GetMethodImageIndex(MethodInfo method)
		{
			if (method.IsPublic)
				return 9;
			if (method.IsPrivate)
				return 10;
			return 11;
		}

		/// <summary>
		/// Gets the image index corresponding to the provided field info.
		/// </summary>
		/// <param name="field">Field info.</param>
		/// <returns>Index of the icon.</returns>
		public static int GetFieldImageIndex(FieldInfo field)
		{
			if (field.IsPublic)
				return 12;
			if (field.IsPrivate)
				return 13;
			return 14;
		}

		/// <summary>
		/// Gets the image index corresponding to the provided event info.
		/// </summary>
		/// <param name="eventInfo">Event info.</param>
		/// <returns>Index of the icon.</returns>
		public static int GetEventImageIndex(EventInfo eventInfo)
		{
			return 15;
		}
	}
}
