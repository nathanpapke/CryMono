using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CryEngine.Native
{
    internal abstract class NativeMethods<T>
    {
        private static T _instance;
        public static T Instance
        {
            get { return _instance; }
            internal set { _instance = value; }
        }

        static NativeMethods()
        {
            _instance = CreateSingletonInstanceByFindingInterfaceImplementation();
        }

        protected static T CreateSingletonInstanceByFindingInterfaceImplementation()
        {
            var interfaceType = typeof(T);

            var implementationType = GetImplementationTypeFromInterface(interfaceType);
            if (implementationType == null)
            {
                throw new NativeInterfaceImplementationNotFoundException(interfaceType);
            }

            return (T)Activator.CreateInstance(implementationType, true);
        }

        private static Type GetImplementationTypeFromInterface(Type interfaceType)
        {
            var implementationType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(typeInfo => typeInfo.GetInterfaces().Any(t => t == interfaceType));
            return implementationType;
        }

    }
}
