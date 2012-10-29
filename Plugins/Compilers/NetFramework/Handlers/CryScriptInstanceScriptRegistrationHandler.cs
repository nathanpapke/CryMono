using System;
using System.Reflection;
using CryEngine.Extensions;
using CryEngine.Initialization;

namespace CryEngine.Compilers.Net.Handlers
{
    internal class CryScriptInstanceScriptRegistrationHandler : IScriptRegistrationParamsHandler
    {
        public IScriptRegistrationParams GetScriptRegistrationParams(Type type)
        {
            foreach (var member in type.GetMethods(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public))
            {
                ConsoleCommandAttribute attribute;
                if (member.TryGetAttribute(out attribute))
                    ConsoleCommand.Register(attribute.Name ?? member.Name, Delegate.CreateDelegate(typeof(ConsoleCommandDelegate), member as MethodInfo) as ConsoleCommandDelegate, attribute.Comment, attribute.Flags);
            }

            return null;
        }
    }
}
