using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using vp.Models;
using vp.Services.Dialogs;
using vp.Services.Settings;

namespace vp.ViewModel
{
    public class PlaylistsViewModel : ViewModelBase
    {
        private readonly IUserSettings _userSettings;
        private readonly IFileDialogService _fileDialogService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private PlaylistCollection _playlistCollection;

        /// <summary>
        /// Bound to PlaylistDataGrid.SelectedItem
        /// </summary>
        public Playlist SelectedPlaylist { get; set; }

        /// <summary>
        /// Current collection of <see cref="Playlist"/>s, acts as a data binding source for View
        /// </summary>
        public PlaylistCollection PlaylistCollection
        {
            get => _playlistCollection;
            set { Set(() => PlaylistCollection, ref _playlistCollection, value); }
        }

        #region Commands

        /// <summary>
        /// Opens an input dialog and adds a new playlist with the input title to the <see cref="PlaylistCollection"/>
        /// </summary>
        public RelayCommand AddPlaylistCommand { get; private set; }

        /// <summary>
        /// Opens an input dialog and renames the provided <see cref="Playlist"/> to the input title
        /// </summary>
        public RelayCommand<Playlist> RenamePlaylistCommand { get; private set; }

        /// <summary>
        /// Opens a file dialog and adds selected videos to the provided playlist
        /// </summary>
        public RelayCommand<Playlist> AddVideosCommand { get; private set; }

        /// <summary>
        /// Removes provided playlists from the <see cref="PlaylistCollection"/>
        /// </summary>
        public RelayCommand<IEnumerable> RemovePlaylistsCommand { get; private set; }

        /// <summary>
        /// Removes provided videos from the <see cref="SelectedPlaylist"/>
        /// </summary>
        public RelayCommand<IEnumerable> RemoveVideosCommand { get; private set; }

        #endregion

        public PlaylistsViewModel(IUserSettings userSettings,
                                  IFileDialogService fileDialogService,
                                  IDialogCoordinator dialogCoordinator)
        {
            _userSettings = userSettings;
            _fileDialogService = fileDialogService;
            _dialogCoordinator = dialogCoordinator;

            PlaylistCollection = _userSettings.PlaylistCollection;

            AddPlaylistCommand = new RelayCommand(OnAddPlaylist);
            AddVideosCommand = new RelayCommand<Playlist>(OnAddVideos);
            RenamePlaylistCommand = new RelayCommand<Playlist>(OnRenamePlaylist);
            RemovePlaylistsCommand = new RelayCommand<IEnumerable>(OnRemovePlaylists);
            RemoveVideosCommand = new RelayCommand<IEnumerable>(OnRemoveVideos);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = ApplicationConstants.AutoSaveTimeSpan;
            timer.Tick += SaveChanges;
            timer.Start();
            PlaylistCollection.ListChanged += SaveChanges;
        }

        #region Command Handlers

        private void OnRemoveVideos(IEnumerable videos)
        {
            if (videos == null) return;

            foreach (var video in videos.OfType<Video>().Reverse())
            {
                if (video != null && (SelectedPlaylist?.Videos?.Contains(video) ?? false))
                {
                    SelectedPlaylist.Videos.Remove(video);
                }
            }
        }

        private void OnRemovePlaylists(IEnumerable playlists)
        {
            if (playlists == null) return;

            foreach (var playlist in playlists.OfType<Playlist>().Reverse())
            {
                if (playlist != null && (PlaylistCollection?.Contains(playlist) ?? false))
                {
                    PlaylistCollection.Remove(playlist);
                }
            }
        }

        private async void OnRenamePlaylist(Playlist playlist)
        {
            string newTitle = await _dialogCoordinator.ShowInputAsync(this, "Rename playlist", "Input a new title for the playlist");
            if (string.IsNullOrEmpty(newTitle) || newTitle == playlist.PlaylistTitle) return;

            if (PlaylistCollection?.CheckTitleUnique(newTitle) ?? false)
            {
                playlist.PlaylistTitle = newTitle;
            }
            
        }
        private void SaveChanges(object s, EventArgs e)
        {
            if (PlaylistCollection != null)
            {
                _userSettings.PlaylistCollection = PlaylistCollection;
                _userSettings.SaveChanges();
            }
        }

        private async void OnAddPlaylist()
        {
            string title = await _dialogCoordinator.ShowInputAsync(this, "Add playlist", "Input a title for the playlist");
            if (string.IsNullOrEmpty(title)) return;

            if (PlaylistCollection?.CheckTitleUnique(title) ?? false)
            {
                PlaylistCollection.Create(title);
            }

        }

        private async void OnAddVideos(Playlist playlist)
        {
            string[] suppVidExt = ApplicationConstants.SupportedVideoExtensions;
            string[] files = await _fileDialogService.OpenVideoFileDialogAsync();
            var videosTask = Task.Run(() =>
            {
                return (files.Where(file => file != null)
                    .Where(file => suppVidExt.Any(ve => file.Contains(ve)))
                    .Select(file => new Video(file))).ToList();
            });
            var pdc = await _dialogCoordinator.ShowProgressAsync(this, "Adding videos...", "Please hold on", false);
            foreach (var video in await videosTask)
            {
                playlist?.Videos?.Add(video);
            }

            await pdc.CloseAsync();
        }

        #endregion 
    }
}
