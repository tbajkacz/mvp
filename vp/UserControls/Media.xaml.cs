using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unosquare.FFME.Events;
using vp.Annotations;
using vp.Models;
using vp.Services.Media;
using vp.Services.Settings;
using vp.ViewModel;

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
        }

        public async Task Play()
        {
            await MediaPlayer.Play();
        }

        public async Task Open(Video video)
        {
            if (video != null)
            {
                await MediaPlayer.Open(new Uri(video.Path));
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

        private void MediaPlayer_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Volume = ((MediaViewModel) DataContext).Volume;
            ((MediaViewModel) DataContext).PropertyChanged += VolumePropertyChanged;
        }
        //TODO ...remove when 4.0.270 ffme works
        private void VolumePropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MediaViewModel.Volume))
            {
                MediaPlayer.Volume = ((MediaViewModel)DataContext).Volume;
            }
        }
    }
}
