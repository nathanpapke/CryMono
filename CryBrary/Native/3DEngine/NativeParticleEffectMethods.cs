using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeParticleEffectMethods : NativeMethods<INativeParticleEffectMethods>, INativeParticleEffectMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr FindEffect(string effectName, bool loadResources);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void Spawn(IntPtr ptr, bool independent, Vec3 pos, Vec3 dir, float scale);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void Remove(IntPtr ptr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void LoadResoruces(IntPtr ptr);
    }
}
