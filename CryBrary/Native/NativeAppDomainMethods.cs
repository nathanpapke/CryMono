using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeAppDomainMethods : INativeAppDomainMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern internal static void _SetScriptAppDomain(int appDomainId);

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void _Initialize();
        
        public void SetScriptAppDomain(int appDomainId)
        {
            _SetScriptAppDomain(appDomainId);
        }

        public void Initialize()
        {
            _Initialize();
        }
    }
}
