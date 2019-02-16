using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GongSolutions.Wpf.DragDrop;
using MahApps.Metro.Controls.Dialogs;
using vp.Messaging;
using vp.Models;
using vp.Services.Dialogs;
using vp.Services.Navigation;
using vp.Services.Settings;
using DragDrop = GongSolutions.Wpf.DragDrop.DragDrop;

namespace vp.ViewModel
{
    public class PlaylistsViewModel : ViewModelBase, IDropTarget
    {
        private readonly IUserSettings _userSettings;
        private readonly IFileDialogService _fileDialogService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IPageNavigationService _pageNavigationService;
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

        

        public PlaylistsViewModel(IUserSettings userSettings,
                                  IFileDialogService fileDialogService,
                                  IDialogCoordinator dialogCoordinator,
                                  IPageNavigationService pageNavigationService)
        {
            _userSettings = userSettings;
            _fileDialogService = fileDialogService;
            _dialogCoordinator = dialogCoordinator;
            _pageNavigationService = pageNavigationService;

            PlaylistCollection = _userSettings.PlaylistCollection;

            AddPlaylistCommand = new RelayCommand(OnAddPlaylist);
            AddVideosCommand = new RelayCommand<Playlist>(OnAddVideos);
            RenamePlaylistCommand = new RelayCommand<Playlist>(OnRenamePlaylist);
            RemovePlaylistsCommand = new RelayCommand<IEnumerable>(OnRemovePlaylists);
            RemoveVideosCommand = new RelayCommand<IEnumerable>(OnRemoveVideos);
            PlayPlaylistCommand = new RelayCommand<Playlist>(OnPlayPlaylist);
            PlayVideoCommand = new RelayCommand<Video>(OnPlayVideo);
            NavigateToMediaPageCommand = new RelayCommand(OnNavigateToMediaPage);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = ApplicationConstants.AutoSaveInterval;
            timer.Tick += SaveChanges;
            timer.Start();
            //PlaylistCollection.ListChanged += SaveChanges;
        }

        #region Commands

        /// <summary>
        /// Opens an input dialog and adds a new playlist with the input title to the <see cref="PlaylistCollection"/>
        /// </summary>
        public RelayCommand AddPlaylistCommand { get; }

        /// <summary>
        /// Opens an input dialog and renames the provided <see cref="Playlist"/> to the input title
        /// </summary>
        public RelayCommand<Playlist> RenamePlaylistCommand { get; }

        /// <summary>
        /// Opens a file dialog and adds selected videos to the provided playlist
        /// </summary>
        public RelayCommand<Playlist> AddVideosCommand { get; }

        /// <summary>
        /// Removes provided playlists from the <see cref="PlaylistCollection"/>
        /// </summary>
        public RelayCommand<IEnumerable> RemovePlaylistsCommand { get; }

        /// <summary>
        /// Removes provided videos from the <see cref="SelectedPlaylist"/>
        /// </summary>
        public RelayCommand<IEnumerable> RemoveVideosCommand { get; }

        /// <summary>
        /// Plays the provided playlist
        /// </summary>
        public RelayCommand<Playlist> PlayPlaylistCommand { get; }

        /// <summary>
        /// Plays the provided video
        /// </summary>
        public RelayCommand<Video> PlayVideoCommand { get; }

        /// <summary>
        /// Navigates to the MediaPlayer page
        /// </summary>
        public RelayCommand NavigateToMediaPageCommand { get; }

        #endregion

        #region Command Handlers

        private void OnNavigateToMediaPage()
        {
            _pageNavigationService.NavigateTo(PageKeys.MediaPage);
        }

        private void OnPlayVideo(Video video)
        {
            if (video != null && SelectedPlaylist != null)
            {
                OnNavigateToMediaPage();
                Messenger.Default.Send(new PlayVideoMessage(SelectedPlaylist, video));
            }
        }

        private void OnPlayPlaylist(Playlist playlist)
        {
            if (playlist?.Videos != null && playlist.CurrentlyPlayingId < playlist.Videos.Count)
            {
                OnNavigateToMediaPage();
                Messenger.Default.Send(new PlayPlaylistMessage(playlist));
            }
        }

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
            if (title == null) return;
            if (title == string.Empty)
            {
                PlaylistCollection.Create(PlaylistCollection.GetUniqueNewPlaylistTitle());
            }
            else if (PlaylistCollection?.CheckTitleUnique(title) ?? false)
            {
                PlaylistCollection.Create(title);
            }

        }

        private async void OnAddVideos(Playlist playlist)
        {
            string[] files = await _fileDialogService.OpenVideoFileDialogAsync();
            await AddVideosToPlaylist(playlist, files);
        }

        /// <summary>
        /// Adds the <see cref="IDataObject"/> if it is a video file and the SelectedPlaylist is not null
        /// </summary>
        /// <param name="e"></param>
        private async void HandleVideosDataGridDrop(IDataObject e)
        {
            var data = e.GetData(DataFormats.FileDrop);
            if (data is string[] paths)
            {
                await AddVideosToPlaylist(SelectedPlaylist, paths);
            }
        }

        private async Task AddVideosToPlaylist(Playlist playlist, string[] files)
        {
            if (playlist == null) return;
            string[] suppVidExt = ApplicationConstants.SupportedVideoExtensions;
            var videosTask = Task.Run(() =>
            {
                return (files.Where(file => file != null)
                    .Where(file => suppVidExt.Any(ve => file.Contains(ve)))
                    .Select(file => new Video(file))).ToList();
            });
            var pdc = await _dialogCoordinator.ShowProgressAsync(this, "Adding videos...", "Please hold on", false);
            foreach (var video in await videosTask)
            {
                playlist.Videos?.Add(video);
            }

            await pdc.CloseAsync();
        }
        #endregion

        #region IDropTarget
        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is IDataObject)
            {
                dropInfo.Effects = DragDropEffects.Move;
            }
            else
            {
                DragDrop.DefaultDropHandler.DragOver(dropInfo);
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            switch (dropInfo.Data)
            {
                case IDataObject d:
                    HandleVideosDataGridDrop(d);
                    break;
                case Video _:
                {
                    Video currentVideo = SelectedPlaylist.Videos[SelectedPlaylist.CurrentlyPlayingId ?? 0];

                    DragDrop.DefaultDropHandler.Drop(dropInfo);

                    //fixes invalid CurrentlyPlayingId caused by moving the videos
                    SelectedPlaylist.CurrentlyPlayingId = SelectedPlaylist.Videos.IndexOf(currentVideo);
                    break;
                }
            }
        }
        #endregion
    }
}
