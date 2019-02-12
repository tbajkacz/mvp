using System;
using System.Threading.Tasks;
using System.Windows;
using vp.Events;
using vp.Models;
using vp.Services.Media;

namespace vp.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Media.xaml
    /// </summary>
    public partial class Media : IMediaService
    {
        public Media()
        {
            InitializeComponent();
            //bug? This is required(atleast on my pc) because DirectSound seems to be failing for some reason
            //https://github.com/unosquare/ffmediaelement/issues/319
            MediaPlayer.RendererOptions.UseLegacyAudioOut = true;
            MediaPlayer.MediaEnded += (s, e) => MediaEnded?.Invoke(s, e);
        }

        public async Task Play()
        {
            await MediaPlayer.Play();
        }

        public async Task Open(Video video, TimeSpan? position)
        {
            if (video != null)
            {
                await MediaPlayer.Open(new Uri(video.Path));
                MediaPlayer.Position = position ?? TimeSpan.Zero;
                MediaOpened?.Invoke(this, new MediaOpenedEventArgs(video));
            }
        }

        public async Task Pause()
        {
            await MediaPlayer.Pause();
        }

        public async Task Stop()
        {
            await MediaPlayer.Stop();
        }

        public bool IsPlaying => MediaPlayer.IsPlaying;

        public event RoutedEventHandler MediaEnded;
        public event EventHandler<MediaOpenedEventArgs> MediaOpened;

        private void MediaPlayer_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
