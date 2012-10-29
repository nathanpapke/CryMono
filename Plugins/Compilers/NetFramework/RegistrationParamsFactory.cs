﻿using System.Linq;
using CryEngine.Initialization;
using System.Collections.Generic;

namespace CryEngine.Compilers.Net
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

        public IEnumerable<IScriptRegistrationParams> GetScriptRegistrationParams(CryScript cryScript)
        {
            return 
                from entry in _map 
                let scriptType = entry.Key 
                let handler = entry.Value 
                where (cryScript.ScriptType & scriptType) == scriptType 
                select handler.GetScriptRegistrationParams(cryScript.Type);
        }
    }
}
