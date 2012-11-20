using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CryEngine.Native
{
    internal class NativeCVarMethods : NativeMethods<INativeCVarMethods>, INativeCVarMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Execute(string command, bool silent);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void RegisterCommand(string name, string description, CVarFlags flags);

        // CVars
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void RegisterCVarFloat(string name, ref float val, float defaultVal, CVarFlags flags,
                                                       string description);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void RegisterCVarInt(string name, ref int val, int defaultVal, CVarFlags flags,
                                                     string description);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void RegisterCVarString(string name, [MarshalAs(UnmanagedType.LPStr)] string val,
                                                        string defaultVal, CVarFlags flags, string description);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern float GetCVarFloat(string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern int GetCVarInt(string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern string GetCVarString(string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetCVarFloat(string name, float value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetCVarInt(string name, int value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetCVarString(string name, string value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern bool HasCVar(string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void HandleException(Exception ex);
    }
}