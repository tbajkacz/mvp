using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using vp.Models;
using vp.Services.Serialization;
using vp.Services.Settings;

namespace vp.Services.Playlists
{
    /// <summary>
    /// make in manage a playlist with methods like next and current
    /// </summary>
    public class PlaylistManager : ObservableObject, IPlaylistManager
    {
        private Playlist _currentPlaylist = new Playlist();

        public Playlist CurrentPlaylist
        {
            get => _currentPlaylist;
            set { Set(() => CurrentPlaylist, ref _currentPlaylist, value); }
        }

        public Video CurrentVideo
        {
            get
            {
                if (CurrentPlaylist.Videos != null && CurrentPlaylist.CurrentlyPlayingId.HasValue)
                {
                    return CurrentPlaylist.Videos[CurrentPlaylist.CurrentlyPlayingId.Value];
                }

                return null;
            }
        }

        public PlaylistManager()
        {
            
        }

        public void SetCurrentPlaylist(Playlist playlist)
        {
            if (playlist != null)
            {
                CurrentPlaylist = playlist;
            }
        }

        public void SetCurrentVideo(Video video)
        {
            if (CurrentPlaylist.Videos != null)
            {
                int index = CurrentPlaylist.Videos.IndexOf(video);

                if (index >= 0)
                {
                    CurrentPlaylist.CurrentlyPlayingId = index;
                }
            }
        }

        public Video Prev()
        {
            if (CurrentPlaylist.CurrentlyPlayingId.HasValue &&
                CurrentPlaylist.CurrentlyPlayingId.Value > 0)
            {
                CurrentPlaylist.CurrentlyPlayingId--;
                return CurrentPlaylist.Videos[CurrentPlaylist.CurrentlyPlayingId.Value];
            }

            return null;
        }

        public Video Next()
        {
            if (CurrentPlaylist.CurrentlyPlayingId.HasValue &&
                CurrentPlaylist.CurrentlyPlayingId.Value < CurrentPlaylist?.Videos?.Count - 1)
            {
                CurrentPlaylist.CurrentlyPlayingId++;
                return CurrentPlaylist.Videos[CurrentPlaylist.CurrentlyPlayingId.Value];
            }

            return null;
        }

    }
}
