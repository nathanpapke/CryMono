using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    interface INativeNetworkMethods
    {
        void RemoteInvocation(uint entityId, uint scriptId, string methodName, object[] args, NetworkTarget target, int channelId);

        bool IsServer();
        bool IsClient();
        bool IsMultiplayer();
    }
}