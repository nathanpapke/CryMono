using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeTimeMethods : NativeMethods<INativeTimeMethods>, INativeTimeMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetTimeScale(float scale);
    }
}
