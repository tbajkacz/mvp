using System.Linq;
using GalaSoft.MvvmLight;
using vp.Models;

namespace vp.Services.Playlists
{
    public class PlaylistCollectionManager : ObservableObject, IPlaylistCollectionManager
    {
        private PlaylistCollection _currentCollection;

        public PlaylistCollection CurrentCollection
        {
            get => _currentCollection;
            set { Set(() => CurrentCollection, ref _currentCollection, value); }
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            if (CurrentCollection != null && playlist != null)
            {
                Playlist retrievedPlaylist = CurrentCollection.FirstOrDefault(p => p.PlaylistTitle == playlist.PlaylistTitle);
                if (retrievedPlaylist != null)
                {
                    CurrentCollection[CurrentCollection.IndexOf(retrievedPlaylist)] = playlist;
                }
            }
        }
    }
}
