using System.Runtime.Serialization;

namespace CryEngine.Serialization
{
    public interface ICrySerializable : ISerializable
    {
        object SetObjectData(object obj, SerializationInfo info, StreamingContext context);
    }
}
