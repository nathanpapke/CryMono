using CryEngine.Initialization;
using System.Collections.Generic;

namespace CryEngine.Compilers.Net.Handlers
{
    internal class ActorScriptRegistrationHandler : IScriptRegistrationParamsHandler
    {
        public IScriptRegistrationParams GetScriptRegistrationParams(System.Type type)
        {
            var actorRegistrationParams = new ActorRegistrationParams();

            return actorRegistrationParams;
        }
    }
}
