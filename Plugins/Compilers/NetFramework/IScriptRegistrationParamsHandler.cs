using CryEngine.Initialization;

namespace CryEngine.Compilers.Net
{
    internal interface IScriptRegistrationParamsHandler
    {
        IScriptRegistrationParams GetScriptRegistrationParams(System.Type type);
    }
}
