using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using vp.Models;
using vp.Services.Media;
using vp.Services.Playlists;
using vp.Services.Settings;

namespace vp.ViewModel
{
    /// <summary>
    /// ViewModel for the <see cref="Media"/>
    /// bug As for version 4.0.260 setting MediaElement.Volume is not working properly - it can only be changed when a video is already running - initial change
    /// bug should be made by using an event. That is fixed in 4.0.270 but Media.Source and Media.Open are currently not working (atleast for me) so workarounds with version 4.0.260 have to be used
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

        public MediaViewModel(IUserSettings userSettings,
                              IPlaylistManager playlistManager)
        {
            _userSettings = userSettings;
            _playlistManager = playlistManager;
            InitializeMediaServiceCommand = new RelayCommand<IMediaService>(OnInitializeMediaService);
            OpenVideoCommand = new RelayCommand<Video>(OnOpenVideo);
            PlayPauseVideoCommand = new RelayCommand(OnPlayPauseVideo);

            LoadSettings();
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
        public RelayCommand<IMediaService> InitializeMediaServiceCommand { get; }

        /// <summary>
        /// Opens the provided <see cref="Video"/> using the <see cref="_mediaService"/>
        /// </summary>
        public RelayCommand<Video> OpenVideoCommand { get; }

        /// <summary>
        /// Play or pauses the <see cref="IMediaService"/> based on <see cref="IMediaService.IsPlaying"/>
        /// </summary>
        public RelayCommand PlayPauseVideoCommand { get; set; }

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
    }
}
