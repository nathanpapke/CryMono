using System.Runtime.Serialization;
using CryEngine.Serialization;
using NUnit.Framework;

namespace CryBrary.Tests.Serialization
{
    [TestFixture]
    public class DefaultSerializationSurrogateTests
    {
        [Test]
        public void GetObjectData_ClassWithSerializableAttributeAndISerializableImplementation_CallsGetObjectData()
        {
            // Arrange
            var objectToSerialize = new ClassWithSerializableAttributeAndISerializableImplementation();
            var surrogate = new DefaultSerializationSurrogate();

            // Act
            surrogate.GetObjectData(objectToSerialize, null, new StreamingContext());

            // Assert
            Assert.IsTrue(objectToSerialize.GetObjectDataCalled);
        }

        [Test]
        public void GetObjectData_ClassWithoutSerializableAttributeButWithISerialzableImplementation_CallsGetObjectData()
        {
            // Arrange
            var objectToSerialize = new ClassWithoutSerializableAttributeButWithISerialzableImplementation();
            var surrogate = new DefaultSerializationSurrogate();

            // Act
            surrogate.GetObjectData(objectToSerialize, null, new StreamingContext());

            // Assert
            Assert.IsTrue(objectToSerialize.GetObjectDataCalled);
        }

        [Test]
        public void SetObjectData_SimpleSerializableClassContainingSerializationConstructor_CallsSerializationConstructor()
        {
            // Arrange
            var objectToSerialize = new SimpleSerializableClassContainingSerializationConstructor();
            var surrogate = new DefaultSerializationSurrogate();
            var info = new SerializationInfo(typeof (SimpleSerializableClassContainingSerializationConstructor), new FormatterConverter() );

            // Act
            surrogate.SetObjectData(objectToSerialize, info, new StreamingContext(), null);

            // Assert
            Assert.IsFalse(objectToSerialize.GetObjectDataCalled);
            Assert.IsTrue(objectToSerialize.SerializationConstructorCalled);
        }

        [Test]
        public void SetObjectData_SimpleCrySerializableClassContainingSerializationConstructor_CallsSerializationConstructor()
        {
            // Arrange
            var objectToSerialize = new SimpleCrySerializableClassContainingSerializationConstructor();
            var surrogate = new DefaultSerializationSurrogate();
            var info = new SerializationInfo(typeof(SimpleCrySerializableClassContainingSerializationConstructor), new FormatterConverter());

            // Act
            surrogate.SetObjectData(objectToSerialize, info, new StreamingContext(), null);

            // Assert
            Assert.IsFalse(objectToSerialize.GetObjectDataCalled);
            Assert.IsFalse(objectToSerialize.SetObjectDataCalled);
            Assert.IsTrue(objectToSerialize.SerializationConstructorCalled);
        }

        [Test]
        public void SetObjectData_SimpleCrySerializableClass_CallsSetObjectData()
        {
            // Arrange
            var objectToSerialize = new SimpleCrySerializableClass();
            var surrogate = new DefaultSerializationSurrogate();
            var info = new SerializationInfo(typeof(SimpleCrySerializableClass), new FormatterConverter());

            // Act
            surrogate.SetObjectData(objectToSerialize, info, new StreamingContext(), null);

            // Assert
            Assert.IsFalse(objectToSerialize.GetObjectDataCalled);
            Assert.IsTrue(objectToSerialize.SetObjectDataCalled);
        }

        [Test]
        public void SetObjectData_SimpleSerializableClass_SerializeAllProperties()
        {
            // Arrange
            var objectToSerialize = new SimpleSerializableClass
                                        {
                                            ValueType = 5, 
                                            ReferenceType = "MyValue"
                                        };
            objectToSerialize.ListOfStuff.Add("Number3");
            var surrogate = new DefaultSerializationSurrogate();
            var info = new SerializationInfo(typeof(SimpleSerializableClass), new FormatterConverter());
            var streamingContext = new StreamingContext();

            // Act
            objectToSerialize.GetObjectData(info, streamingContext); // We need something to deserialize again

            var deserializedObject = new SimpleCrySerializableClass();
            surrogate.SetObjectData(deserializedObject, info, streamingContext, null);

            // Assert
            Assert.IsTrue(deserializedObject.ValueType == 5);
            Assert.IsTrue(deserializedObject.ReferenceType == "MyValue");
            Assert.IsTrue(deserializedObject.ListOfStuff != null);
            Assert.IsTrue(deserializedObject.ListOfStuff.Contains("Number3"));
            Assert.IsTrue(deserializedObject.ListOfStuff.Count == 3);
        }
    }
}
