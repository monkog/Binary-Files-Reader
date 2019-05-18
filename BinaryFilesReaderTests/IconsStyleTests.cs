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

		private class PrivateClass { }

		protected class ProtectedClass { }

		private interface IPrivateInterface { }

		protected interface IProtectedInterface { }

		public void PublicMethod() { }

		protected void ProtectedMethod() { }

		private void PrivateMethod() { }

		private int _privateField;

		protected int ProtectedField;

		public int PublicField;

		public event EventHandler PublicEvent;

		private enum PrivateEnum { }

		protected enum ProtectedEnum { }
	}

	public sealed class SealedClass { }

	public class PublicClass { }

	public interface IPublicInterface { }

	public enum PublicEnum { }
}
