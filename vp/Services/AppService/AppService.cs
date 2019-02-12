using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using vp.vpWindows;

namespace vp.Services.AppService
{
    public class AppService : IAppService
    {
        public void SetMainWindowFullscreen(bool fullscreen)
        {
            Window window = Application.Current.MainWindow;
            if (!fullscreen)
            {
                window.WindowState = WindowState.Normal;
                window.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
                window.WindowStyle = WindowStyle.None;
            }
        }

        public void ToggleMainWindowFullscreen()
        {
            Window window = Application.Current.MainWindow;
            if (window.WindowStyle == WindowStyle.None)
            {
                window.WindowState = WindowState.Normal;
                window.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
                window.WindowStyle = WindowStyle.None;
            }
        }
    }
}
