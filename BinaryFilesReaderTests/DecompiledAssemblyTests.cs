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
			Assert.IsNotNull(_unitUnderTest.Fields);
			Assert.IsNotNull(_unitUnderTest.Properties);
			Assert.IsNotNull(_unitUnderTest.Methods);
			Assert.IsNotNull(_unitUnderTest.Events);
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

		[Test]
		public void InitializeMethodsForType_ValidType_MethodCollectionAssigned()
		{
			var type = typeof(DecompiledAssemblyTests);
			_unitUnderTest.InitializeMethodsForType(type);

			var contains = _unitUnderTest.Methods.ContainsKey(type);
			var methods = _unitUnderTest.Methods[type];

			Assert.IsTrue(contains);
			Assert.IsNotNull(methods);
		}

		[Test]
		public void InitializeMethodsForType_InvalidType_ExceptionThrown()
		{
			void Initialize() => _unitUnderTest.InitializeMethodsForType(typeof(DecompiledAssembly));

			Assert.Throws<ArgumentException>(Initialize);
		}

		[Test]
		public void InitializeFieldsForType_ValidType_FieldsCollectionAssigned()
		{
			var type = typeof(DecompiledAssemblyTests);
			_unitUnderTest.InitializeFieldsForType(type);

			var contains = _unitUnderTest.Fields.ContainsKey(type);
			var fields = _unitUnderTest.Fields[type];

			Assert.IsTrue(contains);
			Assert.IsNotNull(fields);
		}

		[Test]
		public void InitializeFieldsForType_InvalidType_ExceptionThrown()
		{
			void Initialize() => _unitUnderTest.InitializeFieldsForType(typeof(DecompiledAssembly));

			Assert.Throws<ArgumentException>(Initialize);
		}

		[Test]
		public void InitializePropertiesForType_ValidType_PropertiesCollectionAssigned()
		{
			var type = typeof(DecompiledAssemblyTests);
			_unitUnderTest.InitializePropertiesForType(type);

			var contains = _unitUnderTest.Properties.ContainsKey(type);
			var properties = _unitUnderTest.Properties[type];

			Assert.IsTrue(contains);
			Assert.IsNotNull(properties);
		}

		[Test]
		public void InitializePropertiesForType_InvalidType_ExceptionThrown()
		{
			void Initialize() => _unitUnderTest.InitializePropertiesForType(typeof(DecompiledAssembly));

			Assert.Throws<ArgumentException>(Initialize);
		}

		[Test]
		public void InitializeEventsForType_ValidType_EventsCollectionAssigned()
		{
			var type = typeof(DecompiledAssemblyTests);
			_unitUnderTest.InitializeEventsForType(type);

			var contains = _unitUnderTest.Events.ContainsKey(type);
			var events = _unitUnderTest.Events[type];

			Assert.IsTrue(contains);
			Assert.IsNotNull(events);
		}

		[Test]
		public void InitializeEventsForType_InvalidType_ExceptionThrown()
		{
			void Initialize() => _unitUnderTest.InitializeEventsForType(typeof(DecompiledAssembly));

			Assert.Throws<ArgumentException>(Initialize);
		}
	}
}
