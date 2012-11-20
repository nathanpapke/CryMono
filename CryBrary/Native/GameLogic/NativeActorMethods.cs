using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeActorMethods : NativeMethods<INativeActorMethods>, INativeActorMethods
    {
        #region Externals
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern float GetPlayerHealth(IntPtr actorPtr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetPlayerHealth(IntPtr actorPtr, float newHealth);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern float GetPlayerMaxHealth(IntPtr actorPtr);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetPlayerMaxHealth(IntPtr actorPtr, float newMaxHealth);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern ActorInfo GetActorInfoByChannelId(ushort channelId);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern ActorInfo GetActorInfoById(uint entId);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void RegisterActorClass(string name, bool isNative);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern ActorInfo CreateActor(int channelId, string name, string className, Vec3 pos, Quat rot, Vec3 scale);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void RemoveActor(uint id);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern uint GetClientActorId();
        #endregion
    }
}