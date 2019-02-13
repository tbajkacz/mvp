using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp.Messaging
{
    public class ToggleWindowFullscreenMessage
    {
        /// <summary>
        /// Null - toggle, true - set fullscreen, false - normalize
        /// </summary>
        public readonly bool? Fullscreen;

        public ToggleWindowFullscreenMessage(bool? fullscreen = null)
        {
            Fullscreen = fullscreen;
        }
    }
}
