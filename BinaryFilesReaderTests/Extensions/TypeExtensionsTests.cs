using System;
using BinaryFilesReader.Extensions;
using NUnit.Framework;

namespace BinaryFilesReaderTests.Extensions
{
	[TestFixture]
	public class TypeExtensionsTests
	{
		[Test]
		[TestCase(typeof(bool))]
		[TestCase(typeof(int))]
		[TestCase(typeof(double))]
		[TestCase(typeof(float))]
		[TestCase(typeof(long))]
		[TestCase(typeof(short))]
		[TestCase(typeof(string))]
		public void IsSimpleType_SimpleTypes_True(Type type)
		{
			var result = type.IsSimpleType();

			Assert.IsTrue(result);
		}

		[Test]
		[TestCase(typeof(object))]
		[TestCase(typeof(TypeExtensionsTests))]
		public void IsSimpleType_ComplexTypes_False(Type type)
		{
			var result = type.IsSimpleType();

			Assert.IsFalse(result);
		}

		[Test]
		public void ConvertObject_CanConvert_InstanceReturned()
		{
			var result = typeof(TypeExtensionsTests).ConvertObject(this, out _);

			Assert.IsNotNull(result);
		}

		[Test]
		public void ConvertObject_CanConvert_NoErrorMessage()
		{
			typeof(TypeExtensionsTests).ConvertObject(this, out var error);

			StringAssert.AreEqualIgnoringCase(string.Empty, error);
		}

		[Test]
		public void ConvertObject_CannotConvert_InstanceReturned()
		{
			var result = typeof(TypeExtensions).ConvertObject(this, out _);

			Assert.IsNull(result);
		}

		[Test]
		public void ConvertObject_CannotConvert_ErrorSet()
		{
			typeof(TypeExtensions).ConvertObject(this, out var error);

			Assert.IsNotNull(error);
			Assert.IsFalse(string.IsNullOrEmpty(error) || string.IsNullOrWhiteSpace(error));
		}

		[Test]
		public void ConvertObject_CannotConvert_NoErrorMessage()
		{
			typeof(TypeExtensions).ConvertObject(this, out var error);

			StringAssert.AreNotEqualIgnoringCase(string.Empty, error);
		}
	}
}
