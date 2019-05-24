using System;
using System.Linq;
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
			Icons2017.Images.Add(Properties.Resources.Enumerator_16x);
			Icons2017.Images.Add(Properties.Resources.EnumPrivate_16x);
			Icons2017.Images.Add(Properties.Resources.EnumProtect_16x);
			Icons2017.Images.Add(Properties.Resources.Constant_16x);
			Icons2017.Images.Add(Properties.Resources.ConstantPrivate_16x);
			Icons2017.Images.Add(Properties.Resources.ConstantProtected_16x);
			Icons2017.Images.Add(Properties.Resources.Property_16x);
			Icons2017.Images.Add(Properties.Resources.PropertyPrivate_16x);
			Icons2017.Images.Add(Properties.Resources.Structure_16x);
			Icons2017.Images.Add(Properties.Resources.StructurePrivate_16x);
			Icons2017.Images.Add(Properties.Resources.StructureProtect_16x);
			Icons2017.Images.Add(Properties.Resources.Operator_16x);
			Icons2017.Images.Add(Properties.Resources.Delegate_16x);
			Icons2017.Images.Add(Properties.Resources.DelegateProtected_16x);
			Icons2017.Images.Add(Properties.Resources.DelegatePrivate_16x);

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
			Icons2012.Images.Add(Properties.Resources.Enum_582);
			Icons2012.Images.Add(Properties.Resources.Enum_Private_586);
			Icons2012.Images.Add(Properties.Resources.Enum_Protected_585);
			Icons2012.Images.Add(Properties.Resources.Constant_495);
			Icons2012.Images.Add(Properties.Resources.Constant_Private_519);
			Icons2012.Images.Add(Properties.Resources.Constant_Protected_508);
			Icons2012.Images.Add(Properties.Resources.PropertyIcon);
			Icons2012.Images.Add(Properties.Resources.Property_Private_505);
			Icons2012.Images.Add(Properties.Resources.Structure_507);
			Icons2012.Images.Add(Properties.Resources.Structure_Private_512);
			Icons2012.Images.Add(Properties.Resources.Structure_Protected_511);
			Icons2012.Images.Add(Properties.Resources.Operator_660);
			Icons2012.Images.Add(Properties.Resources.Delegate_540);
			Icons2012.Images.Add(Properties.Resources.Delegate_Protected_573);
			Icons2012.Images.Add(Properties.Resources.Delegate_Private_580);
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
				if (type.BaseType == typeof(MulticastDelegate))
					return GetDelegateImageIndex(type);

				if (type.IsClass)
					return GetClassImageIndex(type);

				if (type.IsInterface)
					return GetInterfaceImageIndex(type);

				if (type.IsEnum)
					return GetEnumImageIndex(type);

				if (type.IsValueType)
					return GetStructImageIndex(type);
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
			if (method.IsSpecialName && method.IsStatic)
				return 27;

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
			// Constant fields
			if (field.IsLiteral && !field.IsInitOnly)
				return GetConstantImageIndex(field);

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

		/// <summary>
		/// Gets the image index corresponding to the provided property info.
		/// </summary>
		/// <param name="property">Property info.</param>
		/// <returns>Index of the icon.</returns>
		public static int GetPropertyImageIndex(PropertyInfo property)
		{
			if (property.GetAccessors(false).Any(a => a.IsPublic))
				return 22;
			return 23;
		}

		private static int GetStructImageIndex(Type type)
		{
			if (type.IsPublic || type.IsNestedPublic)
				return 24;
			if (type.IsNestedFamily)
				return 26;
			return 25;
		}

		private static int GetEnumImageIndex(Type type)
		{
			if (type.IsPublic || type.IsNestedPublic)
				return 16;
			if (type.IsNestedFamily)
				return 18;
			return 17;
		}

		private static int GetInterfaceImageIndex(Type type)
		{
			if (type.IsPublic || type.IsNestedPublic)
				return 5;
			if (type.IsNestedFamily)
				return 7;
			return 6;
		}

		private static int GetClassImageIndex(Type type)
		{
			if (type.IsSealed)
				return 4;
			if (type.IsPublic || type.IsNestedPublic)
				return 1;
			if (type.IsNestedFamily)
				return 3;
			return 2;
		}

		private static int GetDelegateImageIndex(Type type)
		{
			if (type.IsPublic || type.IsNestedPublic)
				return 28;
			if (type.IsNestedFamily)
				return 29;
			return 30;
		}

		private static int GetConstantImageIndex(FieldInfo field)
		{
			if (field.IsPublic)
				return 19;
			if (field.IsPrivate)
				return 20;
			return 21;
		}
	}
}
