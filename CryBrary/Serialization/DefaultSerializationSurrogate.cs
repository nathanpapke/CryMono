using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CryEngine.Serialization
{
    public class DefaultSerializationSurrogate : ISerializationSurrogate
    {
        private static Type[] serializationConstructorTypes = new Type[] { typeof(SerializationInfo), typeof(StreamingContext)};

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            if (obj == null)
                return;
            
            var serializable = obj as ISerializable;
            if (serializable != null)
            {
                serializable.GetObjectData(info, context);
            } else
            {
                throw new NotSupportedException(String.Format("The default serializer cannot serialize {0} because it does not implement ISerializable", obj.ToString()));
            }
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            if (obj == null || info == null || info.ObjectType == null)
                return null;

            // Try to locate the serialization constructor
            var constructor = info.ObjectType.GetConstructor(serializationConstructorTypes);
            if (constructor != null)
            {
                constructor.Invoke(obj, new object[] {info, context});
                return obj;
            } 

            // See if this class implements ICrySerializable
            if(info.ObjectType.GetInterfaces().Any(t => t == typeof(ICrySerializable)))
            {
                var crySerializable = (ICrySerializable) obj;
                crySerializable.SetObjectData(info, context);
                return obj;
            }

            // Try to restore what we can (properties/fields)
            var properties = info.ObjectType.GetProperties();
            var fields = info.ObjectType.GetFields();
            foreach (var item in info)
            {
                var property = properties.FirstOrDefault(p => p.Name == item.Name && p.PropertyType == item.ObjectType);
                if (property != null)
                {
                    property.SetValue(obj, item.Value, null);
                } else
                {
                    var field = fields.FirstOrDefault(f => f.Name == item.Name && f.FieldType == item.ObjectType);
                    if (field != null)
                    {
                        field.SetValue(obj, item.Value);
                    }
                }
            }

            return null;
        }
    }
}
