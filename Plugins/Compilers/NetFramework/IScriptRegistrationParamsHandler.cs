using CryEngine.Initialization;
using System.Collections.Generic;
namespace CryEngine.Compilers.NET
{
    internal interface IScriptRegistrationParamsHandler
    {
        IEnumerable<IScriptRegistrationParams> GetScriptRegistrationParams();
    }
}
