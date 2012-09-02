using System;
using System.IO;
using System.Threading;
using CryEngine.Initialization;
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
            // Initialize creates the ScriptManager
            NativeMethods.AppDomain.Initialize();
        }

        public Stream Serialize()
        {
            if(ScriptManager.Instance == null)
                throw new InvalidOperationException("Cannot serialize because the ScriptManager instance is null");

            return null;
        }

        public bool Deserialize()
        {
            return false;
        }
    }
}
