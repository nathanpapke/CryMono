using System;
using System.Reflection;

namespace CryEngine
{
    /// <summary>
    /// This class is the entry point. C++ creates an instance of this class while it's initializing.
    /// This should be the only class in the main appdomain, and it is responsible for loading, unloading and reloading scripts into appdomains
    /// </summary>
    public class AppDomainManager
    {
        private AppDomain _scriptAppDomain;
        public AppDomain ScriptAppDomain
        {
            get { return _scriptAppDomain; }
        }

        public AppDomainManager()
        {
            InitializeScriptDomain();
        }

        private void InitializeScriptDomain()
        {
            var appDomainSetup = new AppDomainSetup()
                                     {
                                         ShadowCopyFiles =  "true"
                                     };

            var appDomain = AppDomain.CreateDomain("ScriptDomain", null, appDomainSetup);

            var loader =
                appDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location,
                                                      typeof (ScriptLoader).FullName) as ScriptLoader;

            loader.Register();
            loader.Initialize();
        }

        /// <summary>
        /// Reloads all assemblies loaded in the ScriptAppDomain
        /// </summary>
        /// <returns></returns>
        public bool Reload()
        {
            Debug.LogAlways("Reloading");
            return false;
        }


    }
}
