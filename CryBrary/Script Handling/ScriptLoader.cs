using System;
using System.Threading;
using CryEngine.Native;

namespace CryEngine
{
    internal class ScriptLoader : MarshalByRefObject
    {
        public void Register()
        {
            int appDomainId = Thread.GetDomainID();
            Debug.LogAlways("Registering script domain {0} (ID: {1})", AppDomain.CurrentDomain.FriendlyName, appDomainId);
            NativeMethods.AppDomain.SetScriptAppDomain(appDomainId);
        }

        public void Initialize()
        {
            NativeMethods.AppDomain.Initialize();
        }
    }
}
