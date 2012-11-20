using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeInputMethods : NativeMethods<INativeInputMethods>, INativeInputMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void RegisterAction(string actionName);
    }
}
