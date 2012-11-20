using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CryEngine.Native
{
    internal class NativeFlowNodeMethods : NativeMethods<INativeFlowNodeMethods>, INativeFlowNodeMethods
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void RegisterNode(string typeName);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr GetNode(UInt32 graphId, UInt16 nodeId);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void SetRegularlyUpdated(IntPtr nodePtr, bool updated);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool IsPortActive(IntPtr nodePtr, int port);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ActivateOutput(IntPtr nodePtr, int port);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ActivateOutputInt(IntPtr nodePtr, int port, int value);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ActivateOutputFloat(IntPtr nodePtr, int port, float value);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ActivateOutputEntityId(IntPtr nodePtr, int port, uint value);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ActivateOutputString(IntPtr nodePtr, int port, string value);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ActivateOutputBool(IntPtr nodePtr, int port, bool value);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern void ActivateOutputVec3(IntPtr nodePtr, int port, Vec3 value);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern int GetPortValueInt(IntPtr nodePtr, int port);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern float GetPortValueFloat(IntPtr nodePtr, int port);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern uint GetPortValueEntityId(IntPtr nodePtr, int port);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern string GetPortValueString(IntPtr nodePtr, int port);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern bool GetPortValueBool(IntPtr nodePtr, int port);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern Vec3 GetPortValueVec3(IntPtr nodePtr, int port);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern IntPtr GetTargetEntity(IntPtr nodePtr, out uint entId);
    }
}
