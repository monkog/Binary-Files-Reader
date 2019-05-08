using System;
using System.Reflection;
using BinaryFilesReader;
using NUnit.Framework;

namespace BinaryFilesReaderTests
{
	[TestFixture]
	public class DecompiledAssemblyTests
	{
		private readonly string _className = $"{nameof(BinaryFilesReaderTests)}.{nameof(DecompiledAssemblyTests)}";

		private DecompiledAssembly _unitUnderTest;

		[SetUp]
		public void Initialize()
		{
			_unitUnderTest = new DecompiledAssembly(Assembly.GetExecutingAssembly().Location);
		}

		[Test]
		public void Constructor_ValidPath_PropertiesInitialized()
		{
			Assert.IsNotNull(_unitUnderTest.Assembly);
			Assert.IsNotNull(_unitUnderTest.Instances);
			Assert.IsNotNull(_unitUnderTest.Types);
		}

		[Test]
		public void Types_NoParams_ContainsDefinedTypes()
		{
			CollectionAssert.Contains(_unitUnderTest.Types.Keys, _className);
			Assert.AreEqual(typeof(DecompiledAssemblyTests), _unitUnderTest.Types[_className]);
		}

		[Test]
		public void Instantiate_ValidTypeName_InstanceCreated()
		{
			_unitUnderTest.Instantiate(_className);

			var contains = _unitUnderTest.Instances.ContainsKey(_className);
			var instance = _unitUnderTest.Instances[_className];

			Assert.IsTrue(contains);
			Assert.IsNotNull(instance);
			Assert.IsInstanceOf<DecompiledAssemblyTests>(instance);
		}

		[Test]
		public void Instantiate_InvalidTypeName_ExceptionThrown()
		{
			void Instantiate() => _unitUnderTest.Instantiate(_className + _className);

			Assert.Throws<ArgumentException>(Instantiate);
		}
	}
}
