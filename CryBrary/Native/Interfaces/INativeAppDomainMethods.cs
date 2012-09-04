namespace CryEngine.Native
{
    internal interface INativeAppDomainMethods
    {
        void SetScriptAppDomain(int appDomainId);

        void Initialize(CryEngine.Initialization.ScriptManager scriptManager);
    }
}
