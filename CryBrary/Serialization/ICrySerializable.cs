using System.Runtime.Serialization;

namespace CryEngine.Serialization
{
    public interface ICrySerializable : ISerializable
    {
        void SetObjectData(SerializationInfo info, StreamingContext context);
    }
}
