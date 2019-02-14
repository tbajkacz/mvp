using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
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
        private readonly DispatcherTimer _inactivityTimer;

        private readonly Cursor _defaultCursor;

        public Media()
        {
            InitializeComponent();
            //bug? This is required(atleast on my pc) because DirectSound seems to be failing for some reason
            //https://github.com/unosquare/ffmediaelement/issues/319
            MediaPlayer.RendererOptions.UseLegacyAudioOut = true;
            MediaPlayer.MediaEnded += (s, e) => MediaEnded?.Invoke(s, e);

            _defaultCursor = this.Cursor;

            _inactivityTimer = new DispatcherTimer();
            _inactivityTimer.Interval = TimeSpan.FromSeconds(4);
            _inactivityTimer.Tick += OnInactivity;
            _inactivityTimer.Start();

            Application.Current.MainWindow.PreviewMouseMove += OnActivity;
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


        private void OnInactivity(object s, EventArgs e)
        {
            if (MainDockPanel.IsMouseOver) return;

            var animation = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.3)));
            animation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };

            MainDockPanel.BeginAnimation(OpacityProperty, animation);
            this.Cursor = Cursors.None;
        }

        private void OnActivity(object s, MouseEventArgs e)
        {
            _inactivityTimer.Stop();
            var animation = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.3)));
            animation.EasingFunction = new SineEase {EasingMode = EasingMode.EaseInOut};

            MainDockPanel.BeginAnimation(OpacityProperty, animation);
            this.Cursor = _defaultCursor;
            _inactivityTimer.Start();
        }
    }
}
