using System;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using vp.Messaging;
using vp.Models;
using vp.Services.Media;
using vp.Services.Playlists;
using vp.Services.Settings;
using vp.UserControls;

namespace vp.ViewModel
{
    /// <summary>
    /// ViewModel for the <see cref="Media"/>
    /// bug? MediaPlayer.RendererOptions.UseLegacyAudioOut = true; if videos not loading
    /// </summary>
    public class MediaViewModel : ViewModelBase
    {
        private IMediaService _mediaService;
        private double _volume;

        /// <summary>
        /// Contains all user defined settings and data
        /// </summary>
        private readonly IUserSettings _userSettings;

        private readonly IPlaylistManager _playlistManager;

        /// <summary>
        /// Current Media volume
        /// </summary>
        public double Volume
        {
            get => _volume;
            set { Set(() => Volume, ref _volume, value); }
        }

        public Playlist CurrentPlaylist { get; set; }


        public MediaViewModel(IUserSettings userSettings,
                              IPlaylistManager playlistManager)
        {
            _userSettings = userSettings;
            _playlistManager = playlistManager;
            InitializeMediaServiceCommand = new RelayCommand<IMediaService>(OnInitializeMediaService);
            OpenVideoCommand = new RelayCommand<Video>(OnOpenVideo);
            PlayPauseVideoCommand = new RelayCommand(OnPlayPauseVideo);
            PrevVideoCommand = new RelayCommand(OnPrevVideo);
            NextVideoCommand = new RelayCommand(OnNextVideo);
            FullscreenCommand = new RelayCommand(OnFullscreen);

            LoadSettings();
            RegisterMessages();
        }

        

        private void RegisterMessages()
        {
            Messenger.Default.Register<PlayVideoMessage>(this, async msg =>
            {
                if (msg.Playlist?.Videos != null && msg.Video != null)
                {
                    if (!msg.Playlist.Videos.Contains(msg.Video)) throw new InvalidOperationException($"Video must be a part of the Playlist");

                    this.CurrentPlaylist = msg.Playlist;
                    await _mediaService.Open(msg.Video);
                    CurrentPlaylist.CurrentlyPlayingId = CurrentPlaylist.Videos.IndexOf(msg.Video);
                }
            });

            Messenger.Default.Register<PlayPlaylistMessage>(this, async msg =>
            {
                if (msg.Playlist?.Videos != null)
                {
                    if (msg.Playlist.CurrentlyPlayingId == null) msg.Playlist.CurrentlyPlayingId = 0;
                    if (msg.Playlist.CurrentlyPlayingId >= msg.Playlist.Videos.Count) throw new IndexOutOfRangeException("Playlist.CurrentlyPlayingId out of range");
                    this.CurrentPlaylist = msg.Playlist;
                    
                    await _mediaService.Open(CurrentPlaylist.Videos[msg.Playlist.CurrentlyPlayingId.Value]);
                }
            });
        }

        private void LoadSettings()
        {
            Volume = _userSettings.Volume;
        }

        private void SaveSettings()
        {
            _userSettings.Volume = Volume;


            _userSettings.SaveChanges();
        }

        /// <summary>
        /// Initializes the <see cref="IMediaService"/> with the provided <see cref="IMediaService"/>
        /// </summary>
        public RelayCommand<IMediaService> InitializeMediaServiceCommand { get; private set; }

        /// <summary>
        /// Opens the provided <see cref="Video"/> using the <see cref="_mediaService"/>
        /// </summary>
        public RelayCommand<Video> OpenVideoCommand { get; private set; }

        /// <summary>
        /// Play or pauses the <see cref="IMediaService"/> based on <see cref="IMediaService.IsPlaying"/>
        /// </summary>
        public RelayCommand PlayPauseVideoCommand { get; private set; }

        public RelayCommand PrevVideoCommand { get; private set; }

        public RelayCommand NextVideoCommand { get; private set; }

        public RelayCommand FullscreenCommand { get; private set; }

        private void OnInitializeMediaService(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        private async void OnOpenVideo(Video video)
        {
            Video Vid = new Video(@"C:\Users\meni\Desktop\VidForTesting.mp4");
            await _mediaService.Open(Vid);
        }

        private async void OnPlayPauseVideo()
        {
            if (_mediaService.IsPlaying)
            {
                await _mediaService.Pause();
            }
            else
            {
                await _mediaService.Play();
            }
        }

        private async void OnNextVideo()
        {
            if (CurrentPlaylist?.CurrentlyPlayingId != null && 
                CurrentPlaylist?.Videos != null && 
                CurrentPlaylist.CurrentlyPlayingId + 1 < CurrentPlaylist.Videos.Count)
            {
                CurrentPlaylist.CurrentlyPlayingId++;
                await _mediaService.Open(CurrentPlaylist.Videos[CurrentPlaylist.CurrentlyPlayingId.Value]);
            }
        }

        private async void OnPrevVideo()
        {
            if (CurrentPlaylist?.CurrentlyPlayingId != null &&
                CurrentPlaylist?.Videos != null &&
                CurrentPlaylist.CurrentlyPlayingId - 1 >= 0)
            {
                CurrentPlaylist.CurrentlyPlayingId--;
                await _mediaService.Open(CurrentPlaylist.Videos[CurrentPlaylist.CurrentlyPlayingId.Value]);
            }
        }


        private void OnFullscreen()
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
