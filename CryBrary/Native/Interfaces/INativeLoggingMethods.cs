using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryEngine.Native
{
    interface INativeLoggingMethods
    {
        void LogAlways(string msg);
        void Log(string msg);
        void Warning(string msg);
    }
}
