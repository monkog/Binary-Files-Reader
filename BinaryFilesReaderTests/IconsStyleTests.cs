using System;
using System.Linq;
using System.Reflection;
using BinaryFilesReader;
using NUnit.Framework;

namespace BinaryFilesReaderTests
{
	[TestFixture]
	public class IconsStyleTests
	{
		[Test]
		public void GetTypeImageIndex_PublicClass_1()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(PublicClass));

			Assert.AreEqual(1, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_NestedPublicClass_1()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(NestedPublicClass));

			Assert.AreEqual(1, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PrivateClass_2()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(PrivateClass));

			Assert.AreEqual(2, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_ProtectedClass_3()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(ProtectedClass));

			Assert.AreEqual(3, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_SealedClass_4()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(SealedClass));

			Assert.AreEqual(4, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PublicInterface_5()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(IPublicInterface));

			Assert.AreEqual(5, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_NestedPublicInterface_5()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(INestedPublicInterface));

			Assert.AreEqual(5, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PrivateInterface_6()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(IPrivateInterface));

			Assert.AreEqual(6, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_ProtectedInterface_6()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(IProtectedInterface));

			Assert.AreEqual(7, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_Namespace_8()
		{
			var type = Assembly.GetExecutingAssembly().GetType(nameof(BinaryFilesReaderTests));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(8, iconIndex);
		}

		[Test]
		public void GetMethodImageIndex_PublicMethod_9()
		{
			var method = GetType().GetMethods().Single(m => m.Name == nameof(PublicMethod));

			var iconIndex = IconsStyle.GetMethodImageIndex(method);

			Assert.AreEqual(9, iconIndex);
		}

		[Test]
		public void GetMethodImageIndex_PrivateMethod_10()
		{
			var method = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(m => m.Name == nameof(PrivateMethod));

			var iconIndex = IconsStyle.GetMethodImageIndex(method);

			Assert.AreEqual(10, iconIndex);
		}

		[Test]
		public void GetMethodImageIndex_ProtectedMethod_11()
		{
			var method = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(m => m.Name == nameof(ProtectedMethod));

			var iconIndex = IconsStyle.GetMethodImageIndex(method);

			Assert.AreEqual(11, iconIndex);
		}

		[Test]
		public void GetFieldImageIndex_PublicField_12()
		{
			var field = GetType().GetField(nameof(PublicField), BindingFlags.Public | BindingFlags.Instance);

			var iconIndex = IconsStyle.GetFieldImageIndex(field);

			Assert.AreEqual(12, iconIndex);
		}

		[Test]
		public void GetFieldImageIndex_PrivateField_13()
		{
			var field = GetType().GetField(nameof(_privateField), BindingFlags.NonPublic | BindingFlags.Instance);

			var iconIndex = IconsStyle.GetFieldImageIndex(field);

			Assert.AreEqual(13, iconIndex);
		}

		[Test]
		public void GetFieldImageIndex_ProtectedField_14()
		{
			var field = GetType().GetField(nameof(ProtectedField), BindingFlags.NonPublic | BindingFlags.Instance);

			var iconIndex = IconsStyle.GetFieldImageIndex(field);

			Assert.AreEqual(14, iconIndex);
		}

		[Test]
		public void GetEventImageIndex_PublicEvent_15()
		{
			var eventInfo = GetType().GetEvent(nameof(PublicEvent), BindingFlags.Public | BindingFlags.Instance);

			var iconIndex = IconsStyle.GetEventImageIndex(eventInfo);

			Assert.AreEqual(15, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PublicEnum_16()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(PublicEnum));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(16, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_NestedPublicEnum_16()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(NestedPublicEnum));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(16, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PrivateEnum_17()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(PrivateEnum));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(17, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_ProtectedEnum_18()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(ProtectedEnum));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(18, iconIndex);
		}

		[Test]
		public void GetFieldImageIndex_PublicConst_19()
		{
			var field = GetType().GetField(nameof(PublicConst), BindingFlags.Public | BindingFlags.Static);

			var iconIndex = IconsStyle.GetFieldImageIndex(field);

			Assert.AreEqual(19, iconIndex);
		}

		[Test]
		public void GetFieldImageIndex_PrivateConst_20()
		{
			var field = GetType().GetField(nameof(PrivateConst), BindingFlags.NonPublic | BindingFlags.Static);

			var iconIndex = IconsStyle.GetFieldImageIndex(field);

			Assert.AreEqual(20, iconIndex);
		}

		[Test]
		public void GetFieldImageIndex_ProtectedConst_21()
		{
			var field = GetType().GetField(nameof(ProtectedConst), BindingFlags.NonPublic | BindingFlags.Static);

			var iconIndex = IconsStyle.GetFieldImageIndex(field);

			Assert.AreEqual(21, iconIndex);
		}

		[Test]
		public void GetPropertyImageIndex_PublicProperty_22()
		{
			var property = GetType().GetProperty(nameof(PublicProperty), BindingFlags.Public | BindingFlags.Instance);

			var iconIndex = IconsStyle.GetPropertyImageIndex(property);

			Assert.AreEqual(22, iconIndex);
		}

		[Test]
		public void GetPropertyImageIndex_PrivateProperty_23()
		{
			var property = GetType().GetProperty(nameof(PrivateProperty), BindingFlags.NonPublic | BindingFlags.Instance);

			var iconIndex = IconsStyle.GetPropertyImageIndex(property);

			Assert.AreEqual(23, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PublicStruct_24()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(PublicStruct));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(24, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_NestedPublicStruct_24()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(NestedPublicStruct));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(24, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PrivateStruct_25()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(PrivateStruct));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(25, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_ProtectedStruct_26()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(ProtectedStruct));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(26, iconIndex);
		}

		[Test]
		public void GetMethodImageIndex_Operator_27()
		{
			var method = GetType().GetMethods(BindingFlags.Public | BindingFlags.Static).Single(m => m.IsSpecialName);

			var iconIndex = IconsStyle.GetMethodImageIndex(method);

			Assert.AreEqual(27, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PublicDelegate_28()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(PublicDelegate));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(28, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_NestedPublicDelegate_28()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(NestedPublicDelegate));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(28, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_ProtectedDelegate_29()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(ProtectedDelegate));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(29, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_PrivateDelegate_30()
		{
			var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name == nameof(PrivateDelegate));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(30, iconIndex);
		}

		private class PrivateClass { }

		protected class ProtectedClass { }

		public class NestedPublicClass { }

		private interface IPrivateInterface { }

		protected interface IProtectedInterface { }

		public interface INestedPublicInterface { }

		public void PublicMethod() { }

		protected void ProtectedMethod() { }

		private void PrivateMethod() { }

		public static int operator +(IconsStyleTests a) { return 0; }

		private int _privateField;

		protected int ProtectedField;

		public int PublicField;

		private const string PrivateConst = "ʕ• ᴥ •ʔ";

		protected const string ProtectedConst = "ʕ• ᴥ •ʔ";

		public const string PublicConst = "ʕ• ᴥ •ʔ";

		private int PrivateProperty { get; }

		public int PublicProperty { get; }

		public event EventHandler PublicEvent;

		private enum PrivateEnum { }

		protected enum ProtectedEnum { }

		public enum NestedPublicEnum { }

		private struct PrivateStruct { }

		protected struct ProtectedStruct { }

		public struct NestedPublicStruct { }

		private delegate int PrivateDelegate();

		protected delegate int ProtectedDelegate();

		public delegate int NestedPublicDelegate();
	}

	public sealed class SealedClass { }

	public class PublicClass { }

	public interface IPublicInterface { }

	public enum PublicEnum { }

	public struct PublicStruct { }

	public delegate int PublicDelegate();
}
