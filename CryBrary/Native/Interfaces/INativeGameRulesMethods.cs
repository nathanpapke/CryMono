using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    interface INativeGameRulesMethods
    {
        void RegisterGameMode(string gamemode);
        void AddGameModeAlias(string gamemode, string alias);
        void AddGameModeLevelLocation(string gamemode, string location);
        void SetDefaultGameMode(string gamemode);
    }
}
