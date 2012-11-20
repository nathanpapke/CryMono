using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeMaterialMethods : NativeMethods<INativeMaterialMethods>, INativeMaterialMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr FindMaterial(string name);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr CreateMaterial(string name);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr LoadMaterial(string name, bool makeIfNotFound = true, bool nonRemovable = false);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr GetSubMaterial(IntPtr materialPtr, int slot);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetSubmaterialCount(IntPtr materialPtr);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr GetMaterial(IntPtr entityPtr, int slot);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetMaterial(IntPtr entityPtr, IntPtr materialPtr, int slot);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr CloneMaterial(IntPtr materialPtr, int subMtl);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetSurfaceTypeName(IntPtr ptr);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool SetGetMaterialParamFloat(IntPtr ptr, string paramName, ref float v, bool get);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool SetGetMaterialParamVec3(IntPtr ptr, string paramName, ref Vec3 v, bool get);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetShaderParamCount(IntPtr ptr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetShaderParamName(IntPtr ptr, int index);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetShaderParam(IntPtr ptr, string paramName, float newVal);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetShaderParam(IntPtr ptr, string paramName, Color newVal);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern MaterialFlags GetFlags(IntPtr ptr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetFlags(IntPtr ptr, MaterialFlags flags);
    }
}
