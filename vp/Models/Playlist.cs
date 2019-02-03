using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace vp.Models
{
    public class Playlist : ObservableObject
    {
        private int? _currentlyPlayingId;
        private string _playlistTitle;
        private ObservableCollection<Video> _videos;

        [JsonProperty("playlistTitle")]
        public string PlaylistTitle
        {
            get => _playlistTitle;
            set { Set(() => PlaylistTitle, ref _playlistTitle, value); }
        }

        [JsonProperty("videos")]
        public ObservableCollection<Video> Videos
        {
            get => _videos;
            set { Set(() => Videos, ref _videos, value); }
        }

        [JsonProperty("currentlyPlayingId")]
        public int? CurrentlyPlayingId
        {
            get => _currentlyPlayingId;
            set { Set(() => CurrentlyPlayingId, ref _currentlyPlayingId, value); }
        }


        public Playlist()
        {
            Videos = new ObservableCollection<Video>();
        }

    }
}
