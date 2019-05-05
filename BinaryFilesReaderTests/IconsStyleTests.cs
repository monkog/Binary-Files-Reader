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
		public void GetTypeImageIndex_SealedClass_0()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(SealedClass));

			Assert.AreEqual(0, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_Class_1()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(Class));

			Assert.AreEqual(1, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_Interface_2()
		{
			var iconIndex = IconsStyle.GetTypeImageIndex(typeof(IInterface));

			Assert.AreEqual(2, iconIndex);
		}

		[Test]
		public void GetTypeImageIndex_Namespace_6()
		{
			var type = Assembly.GetExecutingAssembly().GetType(nameof(BinaryFilesReaderTests));

			var iconIndex = IconsStyle.GetTypeImageIndex(type);

			Assert.AreEqual(6, iconIndex);
		}

		[Test]
		public void GetMethodImageIndex_PrivateMethod_3()
		{
			var method = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(m => m.Name == nameof(PrivateMethod));

			var iconIndex = IconsStyle.GetMethodImageIndex(method);

			Assert.AreEqual(3, iconIndex);
		}

		[Test]
		public void GetMethodImageIndex_ProtectedMethod_4()
		{
			var method = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(m => m.Name == nameof(ProtectedMethod));

			var iconIndex = IconsStyle.GetMethodImageIndex(method);

			Assert.AreEqual(4, iconIndex);
		}

		[Test]
		public void GetMethodImageIndex_PublicMethod_5()
		{
			var method = GetType().GetMethods().Single(m => m.Name == nameof(PublicMethod));

			var iconIndex = IconsStyle.GetMethodImageIndex(method);

			Assert.AreEqual(5, iconIndex);
		}

		private sealed class SealedClass { }

		private class Class { }

		private interface IInterface { }

		public void PublicMethod() { }

		protected void ProtectedMethod() { }

		private void PrivateMethod() { }
	}
}
