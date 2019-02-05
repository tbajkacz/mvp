using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using vp.Models;
using vp.Services.Dialogs;
using vp.Services.Playlists;
using vp.Services.Settings;

namespace vp.ViewModel
{
    public class PlaylistsViewModel : ViewModelBase
    {
        private readonly IUserSettings _userSettings;
        private readonly IFileDialogService _fileDialogService;
        private PlaylistCollection _playlistCollection;
        private bool _isSelected;
        private bool _isItemSelected;

        public PlaylistCollection PlaylistCollection
        {
            get => _playlistCollection;
            set { Set(() => PlaylistCollection, ref _playlistCollection, value); }
        }

        public RelayCommand AddPlaylistCommand { get; set; }

        public RelayCommand<Playlist> AddVideoCommand { get; set; }

        public PlaylistsViewModel(IUserSettings userSettings,
                                  IFileDialogService fileDialogService)
        {
            _userSettings = userSettings;
            _fileDialogService = fileDialogService;

            PlaylistCollection = userSettings.PlaylistCollection;

            AddPlaylistCommand = new RelayCommand(OnAddPlaylist);
            AddVideoCommand = new RelayCommand<Playlist>(OnAddVideo);
        }

        private void OnAddPlaylist()
        {
            
        }

        private async void OnAddVideo(Playlist playlist)
        {
            string[] files = await _fileDialogService.OpenVideoFileDialogAsync();

        }

        public bool IsItemSelected
        {
            get => _isItemSelected;
            set { Set(() => IsItemSelected, ref _isItemSelected, value); }
        }
    }
}
