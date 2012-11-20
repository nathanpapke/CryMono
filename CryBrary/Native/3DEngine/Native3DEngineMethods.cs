using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class Native3DEngineMethods : NativeMethods<INative3DEngineMethods>, INative3DEngineMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern float GetTerrainElevation(float positionX, float positionY, bool includeOutdoorVoxels);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetTerrainSize();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetTerrainSectorSize();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetTerrainUnitSize();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetTerrainZ(int x, int y);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetTimeOfDay(float hour, bool forceUpdate = false);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern float GetTimeOfDay();

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern TimeOfDay.AdvancedInfo GetTimeOfDayAdvancedInfo();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetTimeOfDayAdvancedInfo(TimeOfDay.AdvancedInfo advancedInfo);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetTimeOfDayVariableValue(int id, float value);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetTimeOfDayVariableValueColor(int id, Vec3 value);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ActivatePortal(Vec3 pos, bool activate, string entityName);
    }
}
