using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace vp.Models
{
    public class Playlist : ObservableObject
    {
        private int? _currentlyPlayingId;
        private string _playlistTitle;
        private BindingList<Video> _videos;

        [JsonProperty("playlistTitle")]
        public string PlaylistTitle
        {
            get => _playlistTitle;
            set { Set(() => PlaylistTitle, ref _playlistTitle, value); }
        }

        [JsonProperty("videos")]
        public BindingList<Video> Videos
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
            Videos = new BindingList<Video>();
        }

    }
}
