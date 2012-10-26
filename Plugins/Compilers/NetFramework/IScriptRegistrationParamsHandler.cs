using CryEngine.Initialization;

namespace CryEngine.Compilers.NET
{
    internal interface IScriptRegistrationParamsHandler
    {
        IScriptRegistrationParams GetScriptRegistrationParams(System.Type type);
    }
}
