using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            Debug.LogAlways("Serializing in " + AppDomain.CurrentDomain.FriendlyName);
            if(ScriptManager.Instance == null)
                throw new InvalidOperationException("Cannot serialize because the ScriptManager instance is null");
            
            // What do we need to serialize here? :x
            var i = 20;

            var memoryStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();

            formatter.Serialize(memoryStream,i);
            memoryStream.Position = 0;

            return memoryStream;
        }

        /// <summary>
        /// Executes the action in the current appdomain
        /// </summary>
        /// <param name="action"></param>
        public void Execute(Action action)
        {
            action();
        }

        public bool Deserialize()
        {
            return false;
        }
    }
}
