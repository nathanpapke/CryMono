using System;
using System.IO;
using System.Reflection;
using System.Threading;
using CryEngine.Native;

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
            InitializeScriptDomain(PathUtils.CryMonoFolder);
        }

        public void InitializeScriptDomain(string appDomainRootPath)
        {
            Debug.LogAlways("Initializing script domain from {0} ({1})", AppDomain.CurrentDomain.FriendlyName, System.Threading.Thread.GetDomainID());
            bool initialLoad = ScriptAppDomain == null;
            string domainName = "ScriptDomain_" + ++ScriptDomainCounter;
            var appDomainSetup = new AppDomainSetup()
              {
                  ApplicationBase = appDomainRootPath,

              };

            _scriptAppDomain = AppDomain.CreateDomain(domainName, AppDomain.CurrentDomain.Evidence, appDomainSetup);

            _loader =
                _scriptAppDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location,
                                                      typeof(ScriptLoader).FullName) as ScriptLoader;

            if (_loader == null)
                throw new InvalidOperationException("Failed to initialize the scriptloader");

            OnScriptDomainCreated();

            int appDomainId = _loader.Register();
            _loader.Initialize(initialLoad);

            NativeMethods.AppDomain.SetScriptAppDomain(appDomainId);


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
            bool switchedSuccessfully = false;
            var previousAppdomain = _scriptAppDomain;
            try
            {
                using (Stream currentSerializationStream = _loader.Serialize())
                {
                    // Initialize a new scriptdomain
                    InitializeScriptDomain(AppDomain.CurrentDomain.BaseDirectory);
                    switchedSuccessfully = _loader.Deserialize(currentSerializationStream);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed to reload, keeping original scripts. Exception details:");
                Debug.LogException(e);
                return false;
            }
            finally
            {
                if (switchedSuccessfully)
                {
                    AppDomain.Unload(previousAppdomain);
                }
            }


            return switchedSuccessfully;
        }


    }
}
