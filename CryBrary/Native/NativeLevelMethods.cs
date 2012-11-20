using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeLevelMethods : NativeMethods<INativeLevelMethods>, INativeLevelMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr GetCurrentLevel();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr LoadLevel(string name);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool IsLevelLoaded();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void UnloadLevel();

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetName(IntPtr levelPtr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetPath(IntPtr levelPtr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetPaks(IntPtr levelPtr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetDisplayName(IntPtr levelPtr);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetHeightmapSize(IntPtr levelPtr);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetGameTypeCount(IntPtr levelPtr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetGameType(IntPtr levelPtr, int index);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool SupportsGameType(IntPtr levelPtr, string gameTypeName);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetDefaultGameType(IntPtr levelPtr);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool HasGameRules(IntPtr levelPtr);
    }
}
