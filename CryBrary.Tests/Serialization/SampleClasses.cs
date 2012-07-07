using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using CryEngine.Serialization;

namespace CryBrary.Tests.Serialization
{
    // Basic class
    internal class SimpleClass
    {
        public SimpleClass()
        {
            ValueType = 200;
            ReferenceType = "Hello";
            ListOfStuff = new List<string>(new[]
                                               {
                                                   "Hello1",
                                                   "Hello2"
                                               });
        }

        public int ValueType { get; set; }
        public string ReferenceType { get; set; }
        public List<string> ListOfStuff { get; set; }
    }

    internal class SimpleSerializableClassContainingSerializationConstructor : SimpleClass, ISerializable
    {
        public bool SerializationConstructorCalled = false;
        public bool GetObjectDataCalled = false;

        public SimpleSerializableClassContainingSerializationConstructor()
            : base()
        {
        }

        public SimpleSerializableClassContainingSerializationConstructor(SerializationInfo info, StreamingContext context)
            : base()
        {
            SerializationConstructorCalled = true;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectDataCalled = true;
        }
    }

    internal class SimpleSerializableClass : SimpleClass, ISerializable
    {
        public bool GetObjectDataCalled;
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectDataCalled = true;

            foreach (var property in this.GetType().GetProperties())
            {
                info.AddValue(property.Name, property.GetValue(this,null));
            }
        }
    }


    internal class SimpleCrySerializableClass : SimpleClass, ICrySerializable
    {
        public bool SetObjectDataCalled;
        public bool GetObjectDataCalled;
        
        public SimpleCrySerializableClass()
            : base()
        {
            GetObjectDataCalled = false;
            SetObjectDataCalled = false;
        }


        public void SetObjectData(SerializationInfo info, StreamingContext context)
        {
            SetObjectDataCalled = true;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectDataCalled = true;
        }
    }

    internal class SimpleCrySerializableClassContainingSerializationConstructor : SimpleCrySerializableClass
    {
        public bool SerializationConstructorCalled = false;

        public SimpleCrySerializableClassContainingSerializationConstructor()
            : base()
        {
        }

        public SimpleCrySerializableClassContainingSerializationConstructor(SerializationInfo info, StreamingContext context)
            : base()
        {
            SerializationConstructorCalled = true;
        }
    }



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

    internal class ClassInheritingFromClassImplementingISerializable : ClassWithSerializableAttributeAndISerializableImplementation
    {
        public int HelloThere { get; set; }
    }

    internal class ClassWithoutSerializableAttributeButWithISerialzableImplementation : ISerializable
    {
        public bool GetObjectDataCalled;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectDataCalled = true;
        }
    }



}
