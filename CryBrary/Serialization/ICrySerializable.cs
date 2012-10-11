using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CryEngine.Serialization
{
    public interface ICrySerializable : ISerializable
    {
        object SetObjectData(object obj, SerializationInfo info, StreamingContext context);
    }
}
