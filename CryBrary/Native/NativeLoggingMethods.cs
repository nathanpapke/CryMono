using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace CryEngine.Native
{
    class NativeLoggingMethods : NativeMethods<INativeLoggingMethods>, INativeLoggingMethods
    {
        // Since these methods are using DllImport, the methods need to be static and we need to create non-static methods for these.
        [SuppressUnmanagedCodeSecurity]
        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0"), DllImport("CryMono.dll")]
        private static extern void LogAlways(string msg);
        [SuppressUnmanagedCodeSecurity]
        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0"), DllImport("CryMono.dll")]
        private static extern void Log(string msg);
        [SuppressUnmanagedCodeSecurity]
        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0"), DllImport("CryMono.dll")]
        private static extern void Warning(string msg);

        
        void INativeLoggingMethods.LogAlways(string msg)
        {
            NativeLoggingMethods.LogAlways(msg);
        }

        void INativeLoggingMethods.Log(string msg)
        {
            NativeLoggingMethods.Log(msg);
        }

        void INativeLoggingMethods.Warning(string msg)
        {
            NativeLoggingMethods.Warning(msg);
        }
    }
}