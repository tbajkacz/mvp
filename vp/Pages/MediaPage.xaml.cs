using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unosquare.FFME;
using vp.Models;
using vp.Services.Media;

namespace vp.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy MediaPage.xaml
    /// </summary>
    public partial class MediaPage
    {
        public MediaPage()
        {
            InitializeComponent();
            ////bug? This is required(atleast on my pc) because DirectSound seems to be failing for some reason
            ////https://github.com/unosquare/ffmediaelement/issues/319
            //MediaPlayer.RendererOptions.UseLegacyAudioOut = true;
            //MediaPlayer.MediaEnded += (s, e) => MediaEnded?.Invoke(s, e);
        }

        //public async Task Play()
        //{
        //    await MediaPlayer.Play();
        //}

        //public async Task Open(Video video)
        //{
        //    MediaEngine.LoadFFmpeg();
        //    if (video != null)
        //    {
        //        await MediaPlayer.Open(new Uri(video.Path));
        //    }
        //}

        //public async Task Pause()
        //{
        //    await MediaPlayer.Pause();
        //}

        //public async Task Stop()
        //{
        //    await MediaPlayer.Stop();
        //}

        //public bool IsPlaying => MediaPlayer.IsPlaying;

        //public event RoutedEventHandler MediaEnded;
    }
}
