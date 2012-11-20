using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeNetworkMethods : NativeMethods<INativeNetworkMethods>, INativeNetworkMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void RemoteInvocation(uint entityId, uint scriptId, string methodName, object[] args, NetworkTarget target, int channelId);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern bool IsServer();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern bool IsClient();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern bool IsMultiplayer();
    }
}