using System;
using System.Runtime.CompilerServices;

namespace CryEngine.Native
{
    internal class NativePhysicsMethods : NativeMethods<INativePhysicsMethods>, INativePhysicsMethods
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr GetPhysicalEntity(IntPtr entityPointer);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int RayWorldIntersection(Vec3 origin, Vec3 dir, EntityQueryFlags objFlags, RayWorldIntersectionFlags flags, out RaycastHit rayHit, int maxHits, object[] skipEnts);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Physicalize(IntPtr entPtr, PhysicalizationParams physicalizationParams);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Sleep(IntPtr entPtr, bool sleep);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void AddImpulse(IntPtr entPtr, pe_action_impulse actionImpulse);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Vec3 GetVelocity(IntPtr entPtr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetVelocity(IntPtr entPtr, Vec3 velocity);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern object[] SimulateExplosion(pe_explosion explosion);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern pe_status_living GetLivingEntityStatus(IntPtr entPtr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern pe_action_impulse GetImpulseStruct();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern pe_player_dimensions GetPlayerDimensionsStruct();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern pe_player_dynamics GetPlayerDynamicsStruct();
    }
}
