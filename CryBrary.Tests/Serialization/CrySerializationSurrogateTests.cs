using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CryEngine.Initialization;
using CryEngine.Serialization;
using Moq;
using Xunit;
namespace CryBrary.Tests.Serialization
{
    public class CrySerializationSurrogateTests
    {
        [Fact]
        public void GetObjectData_ISerializableClass_SerializesByInterface()
        {
            // Arrange
            var serializableMock = new Mock<ISerializable>();
            var surrogate = new CrySerializationSurrogate();
            SerializationInfo serializationInfo = null;
            var streamingContext = new StreamingContext();

            // Act
            surrogate.GetObjectData(serializableMock.Object, serializationInfo, streamingContext);

            // Assert
            serializableMock.Verify(serializable => serializable.GetObjectData(null, streamingContext));
        }

        [Fact]
        public void GetObjectData_ClassWithoutAttributes_BestShotSerialization()
        {
            // Arrange
            var surrogate = new CrySerializationSurrogate();
            var instance = new CryBrary.Tests.Serialization.SampleClasses.ClassWithoutAttributes { A = 430, B = "This is a test" };
            var streamingContext = new StreamingContext();
            var serializationInfo =
                new SerializationInfo(typeof(CryBrary.Tests.Serialization.SampleClasses.ClassWithoutAttributes),
                                      new FormatterConverter());

            // Act
            surrogate.GetObjectData(instance, serializationInfo, streamingContext);

            // Assert
            Assert.Equal(serializationInfo.MemberCount, 2);
        }

        [Fact]
        public void GetObjectData_ComplexClass_SerializesWithoutExceptions()
        {
            // Arrange
            var surrogate = new CrySerializationSurrogate();
            var instance = new CryBrary.Tests.Serialization.SampleClasses.ComplexClass(999)
            {
                SerializableProperty =
                    new SampleClasses.SerializableClass { A = 1, B = "Hello serialization" }
            };
            instance._circularClass = new SampleClasses.CircularClass(instance);
            var streamingContext = new StreamingContext();
            var serializationInfo =
                new SerializationInfo(typeof(CryBrary.Tests.Serialization.SampleClasses.ComplexClass),
                                      new FormatterConverter());

            // Act
            surrogate.GetObjectData(instance, serializationInfo, streamingContext);

            // Assert
            // We don't really care how it's serialized (job of the IFormatter), but no exceptions is good enough for this test
            Assert.True(serializationInfo.MemberCount > 0);

        }

        [Fact]
        public void SetObjectData_ICrySerializableClass_DeserializesByInterface()
        {
            // Arrange
            var serializableMock = new Mock<ICrySerializable>();
            var surrogate = new CrySerializationSurrogate();
            SerializationInfo serializationInfo = null;
            var streamingContext = new StreamingContext();

            // Act
            
            var returnValue = surrogate.SetObjectData(serializableMock.Object, serializationInfo, streamingContext, new CrySurrogateSelector());

            // Assert
            serializableMock.Verify(x => x.SetObjectData(serializableMock.Object, serializationInfo, streamingContext));
        }

        [Fact]
        public void SetObjectData_SerializableClassWithSerializationConstructor_DeserializesByConstructor()
        {
            // Arrange
            var obj = new CryBrary.Tests.Serialization.SampleClasses.SerializableClassWithSerializationConstructor()
                          {A = 20, B = "Hello"};
            var surrogate = new CrySerializationSurrogate();
            var serializationInfo =
                new SerializationInfo(typeof(CryBrary.Tests.Serialization.SampleClasses.SerializableClassWithSerializationConstructor),
                                      new FormatterConverter());
            var streamingContext = new StreamingContext();

            // Act

            var returnValue = surrogate.SetObjectData(obj, serializationInfo, streamingContext, new CrySurrogateSelector()) as SampleClasses.SerializableClassWithSerializationConstructor;

            // Assert
            Assert.NotNull(returnValue);

            // Just to know if our constructor has been called
            Assert.Equal(returnValue.A, 20);
            Assert.Equal(returnValue.B, "Hello");
            Assert.Equal(returnValue, obj);
        }

        [Fact]
        public void SetObjectData_ClassWithoutAttributes_BestShotDeserialization()
        {
            // Arrange
            var surrogate = new CrySerializationSurrogate();
            var instance = new CryBrary.Tests.Serialization.SampleClasses.ClassWithoutAttributes { A = 430, B = "This is a test" };
            var streamingContext = new StreamingContext();
            var serializationInfo =
                new SerializationInfo(typeof(CryBrary.Tests.Serialization.SampleClasses.ClassWithoutAttributes),
                                      new FormatterConverter());

            // Act
            var returnValue = surrogate.SetObjectData(instance, serializationInfo, streamingContext, new SurrogateSelector());

            // Assert
            Assert.Equal(returnValue, instance); 
            Assert.NotNull(returnValue);
            Assert.IsType<SampleClasses.ClassWithoutAttributes>(returnValue);

        }

        [Fact]
        public void SetObjectData_ComplexClass_DeserializesWithoutExceptions()
        {
            // Arrange
            var surrogate = new CrySerializationSurrogate();
            var instance = new CryBrary.Tests.Serialization.SampleClasses.ComplexClass(999)
            {
                SerializableProperty =
                    new SampleClasses.SerializableClass { A = 1, B = "Hello serialization" }
            };
            instance._circularClass = new SampleClasses.CircularClass(instance);
            var streamingContext = new StreamingContext();
            var serializationInfo =
                new SerializationInfo(typeof(CryBrary.Tests.Serialization.SampleClasses.ComplexClass),
                                      new FormatterConverter());

            // Act
            var returnValue = surrogate.SetObjectData(instance, serializationInfo, streamingContext, new SurrogateSelector());

            // Assert
            // We don't really care how it's deserialized (job of the IFormatter), but no exceptions is good enough for this test
            Assert.Equal(returnValue, instance);
            Assert.NotNull(returnValue);
            Assert.IsType<SampleClasses.ComplexClass>(returnValue);

        }
    }
}
