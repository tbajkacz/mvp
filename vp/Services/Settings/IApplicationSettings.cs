using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp.Services.Settings
{
    /// <summary>
    /// Contains pre-defined settings which are required for correct functioning
    /// </summary>
    public interface IApplicationSettings
    {
        /// <summary>
        /// Path to a folder containing FFmpeg dlls and executables
        /// </summary>
        string FFmpegPath { get; }
    }
}
