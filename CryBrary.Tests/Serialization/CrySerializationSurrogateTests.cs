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
            var instance = new CryBrary.Tests.Serialization.SampleClasses.ClassWithoutAttributes
                               {A = 430, B = "This is a test"};
            var streamingContext = new StreamingContext();
            var serializationInfo =
                new SerializationInfo(typeof (CryBrary.Tests.Serialization.SampleClasses.ClassWithoutAttributes),
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
                                       new SampleClasses.SerializableClass {A = 1, B = "Hello serialization"}
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
    }
}
