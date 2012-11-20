using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    interface INativeItemSystemMethods
    {
        void CacheItemGeometry(string itemClass);
        void CacheItemSound(string itemClass);

        void GiveItem(uint entityId, string itemClass);
        void GiveEquipmentPack(uint entityId, string equipmentPack);
    }
}
