using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq;

namespace CryEngine.Serialization
{
    public class CrySerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            // Use default ISerializable behavior, if available
            var serializableObj = obj as ISerializable;
            if (serializableObj != null)
            {
                serializableObj.GetObjectData(info, context);
            }
            else
            {
                // Do our custom serialization here!
                var type = obj.GetType();

                var fields = GetFields(type);

                foreach (var fieldInfo in fields)
                {
                    info.AddValue(fieldInfo.Name, fieldInfo.GetValue(obj));
                }
                

            }
        }

        private static IEnumerable<FieldInfo> GetFields(Type type)
        {
            var fields = new List<FieldInfo>();
            fields.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic));
            if (type.BaseType != null)
            {
                var fieldsFromBaseType = GetFields(type.BaseType);
                foreach (var baseFieldInfo in fieldsFromBaseType)
                {
                    if (fields.All(f => f.Name != baseFieldInfo.Name))
                    {
                        fields.Add(baseFieldInfo);
                    }
                }
            }
            


            return fields;
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            // Is this class ICrySerializable
            var crySerializableObj = obj as ICrySerializable;
            if (crySerializableObj != null)
            {
                return crySerializableObj.SetObjectData(obj, info, context);
            }

            // Is this class ISerializable and does it have a serialization constructor?
            var serializableObj = obj as ISerializable;
            if (serializableObj != null)
            {
                
            }

            // Do our custom serialization here!
            var type = obj.GetType();
           
            foreach (var fieldInfo in GetFields(type))
            {
                foreach (SerializationEntry entry in info)
                {
                    if (fieldInfo.Name == entry.Name)
                    {
                        fieldInfo.SetValue(obj, entry.Value);
                        break;
                    }
                }
            }

            return obj;
        }

        
    }
}
