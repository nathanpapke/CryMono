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

        [Fact]
        public void GetSurrogate_SerializableClass_DefaultSerialization()
        {
            // Arrange
            ISurrogateSelector ss = new CrySurrogateSelector();
            ISurrogateSelector usedSurrogateSelector = null;
            ISerializationSurrogate serializationSurrogate;
            var streamingContext = new StreamingContext();

            // Act
            serializationSurrogate = ss.GetSurrogate(typeof(SampleClasses.SerializableClass), streamingContext, out usedSurrogateSelector);

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
            serializationSurrogate = ss.GetSurrogate(typeof(SampleClasses.SerializableClassByInterface), streamingContext, out usedSurrogateSelector);

            // Assert
            //Assert.IsType<CrySerializationSurrogate>(serializationSurrogate);
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
            serializationSurrogate = ss.GetSurrogate(typeof(SampleClasses.ClassInheritingFromSerializableClass), streamingContext, out usedSurrogateSelector);

            // Assert
            //Assert.IsType<CrySerializationSurrogate>(serializationSurrogate);
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
            serializationSurrogate = ss.GetSurrogate(typeof(SampleClasses.ClassWithoutAttributes), streamingContext, out usedSurrogateSelector);

            // Assert
            //Assert.IsType<CrySerializationSurrogate>(serializationSurrogate);
            Assert.Equal(ss, usedSurrogateSelector);
        }
    }
}
