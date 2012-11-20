using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeGameRulesMethods : NativeMethods<INativeGameRulesMethods>, INativeGameRulesMethods
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void RegisterGameMode(string gamemode);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void AddGameModeAlias(string gamemode, string alias);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void AddGameModeLevelLocation(string gamemode, string location);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetDefaultGameMode(string gamemode);
    }
}
