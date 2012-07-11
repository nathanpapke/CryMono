using CryEngine.Initialization;

namespace CryEngine
{
    /// <summary>
    /// These are the methods that can be called from c++
    /// </summary>
    internal interface IScriptManagerCallback
    {
        void PostInit();
        void OnUpdate(float frameTime, float frameStartTime, float asyncTime, float frameRate, float timeScale);
        void OnReload();
        void RemoveInstance(int instanceId, ScriptType scriptType);
    }
}
