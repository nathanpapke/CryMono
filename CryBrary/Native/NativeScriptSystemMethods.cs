using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CryEngine.Native
{
    internal class NativeScriptSystemMethods : NativeMethods<INativeScriptSystemMethods>, INativeScriptSystemMethods
    {
        /// <summary>
        /// Revert the last script reload attempt.
        /// </summary>
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void RevertAppDomain();
        /// <summary>
        /// Attempt to reload scripts again
        /// </summary>
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ReloadAppDomain();
    }
}
