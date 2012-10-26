using CryEngine.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryEngine.Compilers.NET
{
    internal class ScriptRegistrationParamsFactory
    {
        private readonly Dictionary<ScriptType, IScriptRegistrationParamsHandler> _map;

        public ScriptRegistrationParamsFactory()
        {
            _map = new Dictionary<ScriptType, IScriptRegistrationParamsHandler>();
        }

        public void Register<T>(ScriptType type) where T : IScriptRegistrationParamsHandler,new()
        {
            if (_map.ContainsKey(type))
            {
                _map.Remove(type);
            }
            _map.Add(type, new T());
        }



    }
}
