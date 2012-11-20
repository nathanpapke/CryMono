using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryEngine.Native
{
    [Serializable]
    public class NativeInterfaceImplementationNotFoundException : ApplicationException
    {
        public Type InterfaceType { get; set; }

        public NativeInterfaceImplementationNotFoundException() { }
        public NativeInterfaceImplementationNotFoundException(Type interfaceType) { InterfaceType = interfaceType; }
        public NativeInterfaceImplementationNotFoundException(string message) : base(message) { }
        public NativeInterfaceImplementationNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NativeInterfaceImplementationNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
