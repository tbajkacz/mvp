using System;
using System.Windows;
using vp.Services.Settings;

namespace vp
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, EventArgs e)
        {
            Unosquare.FFME.MediaElement.FFmpegDirectory = ApplicationConstants.FFmpegPath;
        }
    }
}
