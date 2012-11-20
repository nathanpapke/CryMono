using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    interface INativeParticleEffectMethods
    {
        IntPtr FindEffect(string effectName, bool loadResources);
        void Spawn(IntPtr ptr, bool independent, Vec3 pos, Vec3 dir, float scale);
        void Remove(IntPtr ptr);
        void LoadResoruces(IntPtr ptr);
    }
}
