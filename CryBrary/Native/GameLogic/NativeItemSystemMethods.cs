using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativeItemSystemMethods : NativeMethods<INativeItemSystemMethods>, INativeItemSystemMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void CacheItemGeometry(string itemClass);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void CacheItemSound(string itemClass);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void GiveItem(uint entityId, string itemClass);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void GiveEquipmentPack(uint entityId, string equipmentPack);
    }
}
