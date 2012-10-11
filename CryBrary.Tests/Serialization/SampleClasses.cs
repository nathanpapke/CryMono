using System;
using System.Runtime.Serialization;

namespace CryBrary.Tests.Serialization
{
    class SampleClasses
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

        public class ComplexClass
        {
            private readonly float _sampleValue;
            public float SampleValue
            {
                get { return _sampleValue; }
            }
            protected int A { get; set; }
            public SerializableClass SerializableProperty { get; set; }
            private SerializableClass serializableClass = new SerializableClass() {A = 3};
            public bool IsFunny
            {
                get { return false; }
            }

            internal CircularClass _circularClass;

            public ComplexClass()
                : this(200)
            {
            }

            public ComplexClass(float value)
            {
                _sampleValue = value;
            }
        }

        public class CircularClass
        {
            private ComplexClass _complexClass;
            public CircularClass(ComplexClass c)
            {
                _complexClass = c;
            }
        }

        public class SerializableClassWithSerializationConstructor : ISerializable
        {
            public int A { get; set; }
            public string B { get; set; }

            public SerializableClassWithSerializationConstructor()
            {
            }

            public SerializableClassWithSerializationConstructor(SerializationInfo info, StreamingContext context)
            {
                A = (int)info.GetValue("A", typeof (int));
                B = (string) info.GetValue("B", typeof (string));
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("A", A);
                info.AddValue("B", B);
            }
        }
    }
}
