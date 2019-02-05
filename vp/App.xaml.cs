using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Ioc;
using vp.Services.Settings;
using vp.ViewModel;

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
