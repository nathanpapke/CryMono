using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CryEngine.Serialization
{
    public class CrySerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            // Use default ISerializable behavior, if available
            ISerializable serializableObj = obj as ISerializable;
            if (serializableObj != null)
            {
                serializableObj.GetObjectData(info, context);
            }
            else
            {
                // Do our custom serialization here!
                
            }
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            // Is this class ICrySerializable
            ICrySerializable crySerializableObj = obj as ICrySerializable;
            if (crySerializableObj != null)
            {
                return crySerializableObj.SetObjectData(obj, info, context);
            }

            // Do our custom serialization here!
            

            return null;
        }
    }
}
