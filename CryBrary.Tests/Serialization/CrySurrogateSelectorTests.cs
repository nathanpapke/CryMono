using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CryEngine.Serialization;
using NUnit.Framework;

namespace CryBrary.Tests.Serialization
{
    [TestFixture]
    public class CrySurrogateSelectorTests : CryBraryTests
    {
        [Serializable]
        internal class ClassWithSerializableAttribute
        {
            public string SerializableProperty { get; set; }

        }

        [Serializable]
        internal class ClassWithSerializableAttributeAndISerializableImplementation : ISerializable
        {
            public bool GetObjectDataCalled;

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                GetObjectDataCalled = true;
            }
        }

        internal class ClassInheritingFromClassWithSerializableAttribute : ClassWithSerializableAttribute
        {
            public int Hello { get; set; }
        }

        internal class ClassWithoutSerializableAttributeButWithISerialzableImplementation : ISerializable
        {
            public bool GetObjectDataCalled;

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                GetObjectDataCalled = true;
            }
        }

        internal class ClassWithoutSerializableAttribute
        {
            public string PropertyA { get; set; }
        }


        [Test]
        public void GetSurrogate_SerializableType_DefaultNullSerialization()
        {
            // Arrange
            var selector = new CrySurrogateSelector();
            ISurrogateSelector selectorResult = null;
            ISerializationSurrogate serializationSurrogate = null;


            // Act
            serializationSurrogate = selector.GetSurrogate(typeof(ClassWithSerializableAttribute), new StreamingContext(), out selectorResult);

            // Assert
            Assert.IsNull(serializationSurrogate);
            Assert.IsNull(selectorResult);
        }

        [Test]
        public void GetSurrogate_SerializableTypeWithISerializableImplementation_DefaultNullSerialization()
        {
            // Arrange
            var selector = new CrySurrogateSelector();
            ISurrogateSelector selectorResult = null;
            ISerializationSurrogate serializationSurrogate = null;

            // Act
            serializationSurrogate = selector.GetSurrogate(typeof(ClassWithSerializableAttributeAndISerializableImplementation), new StreamingContext(), out selectorResult);

            // Assert
            Assert.IsNull(serializationSurrogate);
            Assert.IsNull(selectorResult);
        }

        [Test]
        public void GetSurrogate_TypeWithoutSerializableAttributeButWithISerializableImplementation_DefaultSerializationSurrogate()
        {
            // Arrange
            var selector = new CrySurrogateSelector();
            ISurrogateSelector selectorResult = null;
            ISerializationSurrogate serializationSurrogate = null;

            // Act
            serializationSurrogate = selector.GetSurrogate(typeof(ClassWithoutSerializableAttributeButWithISerialzableImplementation), new StreamingContext(), out selectorResult);

            // Assert
            Assert.IsTrue(serializationSurrogate is DefaultSerializationSurrogate);
        }

        [Test]
        public void GetSurrogate_NonSerializableType_SerializeAllSerializationSurrogate()
        {
            // Arrange
            var selector = new CrySurrogateSelector();
            ISurrogateSelector selectorResult = null;
            ISerializationSurrogate serializationSurrogate = null;

            // Act
            serializationSurrogate = selector.GetSurrogate(typeof(ClassWithoutSerializableAttribute), new StreamingContext(), out selectorResult);

            // Assert
            Assert.IsTrue(serializationSurrogate is SerializeAllSerializationSurrogate);
        }

        [Test]
        public void GetSurrogate_ClassInheritingFromSerializableClass_DefaultSerializationSurrogate()
        {
            // Arrange
            var selector = new CrySurrogateSelector();
            ISurrogateSelector selectorResult = null;
            ISerializationSurrogate serializationSurrogate = null;

            // Act
            serializationSurrogate = selector.GetSurrogate(typeof(ClassInheritingFromClassWithSerializableAttribute), new StreamingContext(), out selectorResult);

            // Assert
            Assert.IsTrue(serializationSurrogate is DefaultSerializationSurrogate);
        }
    }
}
