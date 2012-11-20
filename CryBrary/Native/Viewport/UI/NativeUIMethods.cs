using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeUIMethods : NativeMethods<INativeUIMethods>, INativeUIMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr CreateEventSystem(string name, UI.EventSystemType type);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern uint RegisterFunction(IntPtr eventSystemPtr, string name, string desc, object[] inputs);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern uint RegisterEvent(IntPtr eventSystemPtr, string name, string desc, object[] outputs);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SendEvent(IntPtr eventSystemPtr, uint eventId, object[] args);
    }
}
