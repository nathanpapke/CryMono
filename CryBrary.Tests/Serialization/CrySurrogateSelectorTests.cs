using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using CryEngine.Serialization;
using System.Runtime.Serialization;

namespace CryBrary.Tests.Serialization
{
    public class CrySurrogateSelectorTests
    {
        [Serializable]
        public class SerializableClass
        {
            public int A { get; set; }
            public string B { get; set; }
        }

        public class SerializableClassByInterface : ISerializable
        {
            public int A { get; set; }
            public string B { get; set; }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("A", A);
                info.AddValue("B", B);
            }
        }

        public class ClassInheritingFromSerializableClass : SerializableClass
        {
            public DateTime Timestamp { get; set; }
        }

        public class ClassWithoutAttributes
        {
            public int A { get; set; }
            public string B { get; set; }
        }

        [Fact]
        public void GetSurrogate_SerializableClass_DefaultSerialization()
        {
            // Arrange
            ISurrogateSelector ss = new CrySurrogateSelector();
            ISurrogateSelector usedSurrogateSelector = null;
            ISerializationSurrogate serializationSurrogate;
            var streamingContext = new StreamingContext();

            // Act
            serializationSurrogate = ss.GetSurrogate(typeof(SerializableClass), streamingContext, out usedSurrogateSelector);

            // Assert
            Assert.Null(serializationSurrogate);
            Assert.Null(usedSurrogateSelector);
        }

        [Fact]
        public void GetSurrogate_SerializableClassByInterface_CrySerialization()
        {
            // Arrange
            ISurrogateSelector ss = new CrySurrogateSelector();
            ISurrogateSelector usedSurrogateSelector = null;
            ISerializationSurrogate serializationSurrogate;
            var streamingContext = new StreamingContext();

            // Act
            serializationSurrogate = ss.GetSurrogate(typeof(SerializableClassByInterface), streamingContext, out usedSurrogateSelector);

            // Assert
            Assert.IsType<CrySerializationSurrogate>(serializationSurrogate);
            Assert.Equal(ss, usedSurrogateSelector);
        }

        [Fact]
        public void GetSurrogate_ClassInheritingFromSerializableClass_CrySerialization()
        {
            // Arrange
            ISurrogateSelector ss = new CrySurrogateSelector();
            ISurrogateSelector usedSurrogateSelector = null;
            ISerializationSurrogate serializationSurrogate;
            var streamingContext = new StreamingContext();

            // Act
            serializationSurrogate = ss.GetSurrogate(typeof(ClassInheritingFromSerializableClass), streamingContext, out usedSurrogateSelector);

            // Assert
            Assert.IsType<CrySerializationSurrogate>(serializationSurrogate);
            Assert.Equal(ss, usedSurrogateSelector);
        }

        [Fact]
        public void GetSurrogate_ClassWithoutAttributes_CrySerialization()
        {
            // Arrange
            ISurrogateSelector ss = new CrySurrogateSelector();
            ISurrogateSelector usedSurrogateSelector = null;
            ISerializationSurrogate serializationSurrogate;
            var streamingContext = new StreamingContext();

            // Act
            serializationSurrogate = ss.GetSurrogate(typeof(ClassWithoutAttributes), streamingContext, out usedSurrogateSelector);

            // Assert
            Assert.IsType<CrySerializationSurrogate>(serializationSurrogate);
            Assert.Equal(ss, usedSurrogateSelector);
        }
    }
}
