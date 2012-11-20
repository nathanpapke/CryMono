using System;
using System.Runtime.CompilerServices;
using CryEngine.Lua;

namespace CryEngine.Native
{
    internal class NativeScriptTableMethods : NativeMethods<INativeScriptTableMethods>, INativeScriptTableMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr GetScriptTable(IntPtr entityPtr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr GetSubScriptTable(IntPtr scriptTablePtr, string tableName);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern object CallMethod(IntPtr scriptTablePtr, string methodName, object[] parameters);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern object GetValue(IntPtr scriptTablePtr, string keyName);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool ExecuteBuffer(string buffer);
    }
}
