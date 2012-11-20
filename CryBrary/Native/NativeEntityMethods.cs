using System;
using System.Runtime.CompilerServices;
using CryEngine.Initialization;

namespace CryEngine.Native
{
    internal class NativeEntityMethods : NativeMethods<INativeEntityMethods>, INativeEntityMethods
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void PlayAnimation(IntPtr ptr, string animationName, int slot, int layer, float blend, float speed, AnimationFlags flags);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void StopAnimationInLayer(IntPtr ptr, int slot, int layer, float blendOutTime);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void StopAnimationsInAllLayers(IntPtr ptr, int slot);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern EntityBase SpawnEntity(EntitySpawnParams spawnParams, bool autoInit, out EntityInfo entityInfo);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void RemoveEntity(uint entityId, bool forceRemoveNow = false);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr GetEntity(uint entityId);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern EntityId GetEntityId(IntPtr entPtr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern uint FindEntity(string name);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern object[] GetEntitiesByClass(string className);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern object[] GetEntitiesInBox(BoundingBox bbox, EntityQueryFlags flags);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool RegisterEntityClass(EntityRegistrationParams registerParams);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void BreakIntoPieces(IntPtr ptr, int slot, int piecesSlot, BreakageParameters breakageParams);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern string GetName(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetName(IntPtr ptr, string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern EntityFlags GetFlags(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetFlags(IntPtr ptr, EntityFlags name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void AddMovement(IntPtr animatedCharacterPtr, ref EntityMovementRequest request);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetWorldTM(IntPtr ptr, Matrix34 tm);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Matrix34 GetWorldTM(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetLocalTM(IntPtr ptr, Matrix34 tm);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Matrix34 GetLocalTM(IntPtr ptr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern BoundingBox GetWorldBoundingBox(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern BoundingBox GetBoundingBox(IntPtr ptr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern EntitySlotFlags GetSlotFlags(IntPtr ptr, int slot);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetSlotFlags(IntPtr ptr, int slot, EntitySlotFlags slotFlags);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetWorldPos(IntPtr ptr, Vec3 newPos);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Vec3 GetWorldPos(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetPos(IntPtr ptr, Vec3 newPos);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Vec3 GetPos(IntPtr ptr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetWorldRotation(IntPtr ptr, Quat newAngles);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Quat GetWorldRotation(IntPtr ptr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetRotation(IntPtr ptr, Quat newAngles);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Quat GetRotation(IntPtr ptr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void LoadObject(IntPtr ptr, string fileName, int slot);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern string GetStaticObjectFilePath(IntPtr ptr, int slot);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void LoadCharacter(IntPtr ptr, string fileName, int slot);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern bool AddEntityLink(IntPtr entPtr, string linkName, uint otherId, Quat relativeRot, Vec3 relativePos);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void RemoveEntityLink(IntPtr entPtr, uint otherId);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern int LoadLight(IntPtr entPtr, int slot, LightParams lightParams);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void FreeSlot(IntPtr entPtr, int slot);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern int GetAttachmentCount(IntPtr entPtr, int slot);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr GetAttachmentByIndex(IntPtr entPtr, int index, int slot);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr GetAttachmentByName(IntPtr entPtr, string name, int slot);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void AttachmentUseEntityPosition(IntPtr entityAttachmentPtr, bool use);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void AttachmentUseEntityRotation(IntPtr entityAttachmentPtr, bool use);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr LinkEntityToAttachment(IntPtr attachmentPtr, uint entityId);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern string GetAttachmentObject(IntPtr attachmentPtr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern QuatT GetAttachmentAbsolute(IntPtr attachmentPtr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern QuatT GetAttachmentRelative(IntPtr attachmentPtr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern QuatT GetAttachmentDefaultAbsolute(IntPtr attachmentPtr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern QuatT GetAttachmentDefaultRelative(IntPtr attachmentPtr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr GetAttachmentMaterial(IntPtr attachmentPtr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetAttachmentMaterial(IntPtr attachmentPtr, IntPtr materialPtr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern QuatT GetJointAbsolute(IntPtr entPtr, string jointName, int characterSlot);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern QuatT GetJointAbsoluteDefault(IntPtr entPtr, string jointName, int characterSlot);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern QuatT GetJointRelative(IntPtr entPtr, string jointName, int characterSlot);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern QuatT GetJointRelativeDefault(IntPtr entPtr, string jointName, int characterSlot);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetJointAbsolute(IntPtr entPtr, string jointName, int characterSlot, QuatT absolute);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetTriggerBBox(IntPtr entPtr, BoundingBox bounds);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern BoundingBox GetTriggerBBox(IntPtr entPtr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void InvalidateTrigger(IntPtr entPtr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr AcquireAnimatedCharacter(uint entId);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Hide(IntPtr entityId, bool hide);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern bool IsHidden(IntPtr entityId);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern IntPtr GetEntityFromPhysics(IntPtr physEntPtr);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern EntityUpdatePolicy GetUpdatePolicy(IntPtr entPtr);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetUpdatePolicy(IntPtr entPtr, EntityUpdatePolicy policy);
    }
}
