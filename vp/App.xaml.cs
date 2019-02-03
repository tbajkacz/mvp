using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace vp
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            Unosquare.FFME.MediaElement.FFmpegDirectory = vp.Properties.ApplicationSettings.Default.FFmpegPath;
        }
    }
}
