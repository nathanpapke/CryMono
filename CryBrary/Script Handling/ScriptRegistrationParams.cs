using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryEngine.Initialization
{
    public interface IScriptRegistrationParams { }

    public struct ActorRegistrationParams : IScriptRegistrationParams
    {
    }

    public struct EntityRegistrationParams : IScriptRegistrationParams
    {
        public string Name { get; set; }
        public string Category { get; set; }

        public string EditorHelper { get; set; }
        public string EditorIcon { get; set; }

        public EntityClassFlags Flags { get; set; }

        public EntityProperty[] Properties { get; set; }
    }

    public struct GameRulesRegistrationParams : IScriptRegistrationParams
    {
        public string Name { get; set; }
        public bool DefaultGamemode { get; set; }
    }

    public struct FlowNodeRegistrationParams : IScriptRegistrationParams
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
