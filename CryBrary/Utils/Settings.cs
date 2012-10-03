using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryEngine.Utils
{
    /// <summary>
    /// Class containing global settings
    /// </summary>
    public static class Settings
    {
        static Settings()
        {
            // Default values here
            IsGuiSupported = true;
        }


        /// <summary>
        /// Is a GUI supported?
        /// Should be set to false when running as a dedicated, or when running unit tests
        /// </summary>
        public static bool IsGuiSupported { get; set; }
    }
}
