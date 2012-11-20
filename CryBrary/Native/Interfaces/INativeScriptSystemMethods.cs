using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CryEngine.Native
{
    interface INativeScriptSystemMethods
    {
        /// <summary>
        /// Revert the last script reload attempt.
        /// </summary>
        void RevertAppDomain();
        /// <summary>
        /// Attempt to reload scripts again
        /// </summary>
        void ReloadAppDomain();
    }
}
