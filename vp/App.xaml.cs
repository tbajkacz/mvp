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
        private void OnActivated(object sender, EventArgs e)
        {
            //FFmpeg version 4.0 required
            Unosquare.FFME.MediaElement.FFmpegDirectory = ApplicationConstants.FFmpegPath;
        }
    }
}
