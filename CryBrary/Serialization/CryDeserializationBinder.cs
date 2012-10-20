using System;
using System.Reflection;
using System.Runtime.Serialization;
namespace CryEngine.Serialization
{
    internal class CryDeserializationBinder : SerializationBinder
    {
        public override System.Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            string assemblyNameCurrent = Assembly.GetExecutingAssembly().FullName;

            Debug.LogAlways("Current = " + assemblyNameCurrent + " - Requested = " + assemblyNameCurrent);

            return typeToDeserialize;
        }
    }
}
