using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace vp.Services.AppService
{
    public interface IAppService
    {
        /// <summary>
        /// If true, sets main window to fullscreen, if false, sets window to normalized
        /// </summary>
        /// <param name="fullscreen"></param>
        void SetMainWindowFullscreen(bool fullscreen);

        /// <summary>
        /// Sets to normal if maximized, and to maximized if normal
        /// </summary>
        void ToggleMainWindowFullscreen();

    }
}
