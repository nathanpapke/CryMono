using CryEngine.Extensions;
using CryEngine.Initialization;

namespace CryEngine.Compilers.Net.Handlers
{
    internal class GameRulesScriptRegistrationHandler : IScriptRegistrationParamsHandler
    {
        public IScriptRegistrationParams GetScriptRegistrationParams(System.Type type)
        {
            var gamemodeRegistrationParams = new GameRulesRegistrationParams();

            GameRulesAttribute gamemodeAttribute;
            if (type.TryGetAttribute(out gamemodeAttribute))
            {
                if (!string.IsNullOrEmpty(gamemodeAttribute.Name))
                    gamemodeRegistrationParams.Name = gamemodeAttribute.Name;

                gamemodeRegistrationParams.DefaultGamemode = gamemodeAttribute.Default;
            }

            return gamemodeRegistrationParams;
        }
    }
}
