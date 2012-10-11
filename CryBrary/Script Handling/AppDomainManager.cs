using System;
using System.IO;
using System.Reflection;

namespace CryEngine
{
    /// <summary>
    /// This class is the entry point. C++ creates an instance of this class while it's initializing.
    /// This should be the only class in the main appdomain, and it is responsible for loading, unloading and reloading scripts into appdomains
    /// </summary>
    public class AppDomainManager
    {
        private static int ScriptDomainCounter = 0;

        /// <summary>
        /// Event 
        /// </summary>
        public event EventHandler ScriptDomainCreated;

        private AppDomain _scriptAppDomain;
        /// <summary>
        /// Appdomain for scripts
        /// </summary>
        public AppDomain ScriptAppDomain
        {
            get { return _scriptAppDomain; }
        }

        private ScriptLoader _loader;
        internal ScriptLoader Loader
        {
            get { return _loader; }
        }

        public void InitializeScriptDomain()
        {
            InitializeScriptDomain(null);
        }

        public void InitializeScriptDomain(string appDomainRootPath)
        {
            bool initialLoad = ScriptAppDomain == null;
            var appDomainSetup = new AppDomainSetup()
                                     {
                                         ShadowCopyFiles =  "true",
                                         ApplicationBase = appDomainRootPath
                                     };

            _scriptAppDomain = AppDomain.CreateDomain("ScriptDomain_" + ++ScriptDomainCounter, null, appDomainSetup);

            _loader =
                _scriptAppDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location,
                                                      typeof (ScriptLoader).FullName) as ScriptLoader;

            if (_loader == null)
                throw new InvalidOperationException("Failed to initialize the scriptloader");

            OnScriptDomainCreated();

            _loader.Register();
            _loader.Initialize(initialLoad);
        }

        private void OnScriptDomainCreated()
        {
            if (ScriptDomainCreated != null) ScriptDomainCreated(this, null);
        }

        /// <summary>
        /// Reloads all assemblies loaded in the ScriptAppDomain
        /// 
        /// Note: This is always executed from the main CryMono appdomain, and not from the script domain
        /// </summary>
        /// <returns></returns>
        public bool Reload()
        {
            // TODO: Serialization first
            try
            {
                using (Stream currentSerializationStream = _loader.Serialize())
                {
                    
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }

            InitializeScriptDomain(AppDomain.CurrentDomain.BaseDirectory);

            return true;
        }


    }
}
