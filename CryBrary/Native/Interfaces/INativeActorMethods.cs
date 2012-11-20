using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    interface INativeActorMethods
    {
        float GetPlayerHealth(IntPtr actorPtr);
        void SetPlayerHealth(IntPtr actorPtr, float newHealth);
        float GetPlayerMaxHealth(IntPtr actorPtr);
        void SetPlayerMaxHealth(IntPtr actorPtr, float newMaxHealth);

        ActorInfo GetActorInfoByChannelId(ushort channelId);
        ActorInfo GetActorInfoById(uint entId);

        void RegisterActorClass(string name, bool isNative);
        ActorInfo CreateActor(int channelId, string name, string className, Vec3 pos, Quat rot, Vec3 scale);
        void RemoveActor(uint id);

        uint GetClientActorId();
    }
}