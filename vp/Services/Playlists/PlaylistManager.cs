using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp.Models;
using vp.Services.Serialization;
using vp.Services.Settings;

namespace vp.Services.Playlists
{
    public class PlaylistManager : IPlaylistManager
    {
        private readonly IUserSettings _userSettings;

        public PlaylistManager(IUserSettings userSettings)
        {
            _userSettings = userSettings;
        }


        public void SavePlaylists(PlaylistCollection playlistCollection)
        {
            _userSettings.PlaylistCollection = playlistCollection;
        }

        public PlaylistCollection GetPlaylists()
        {
            return _userSettings.PlaylistCollection;
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            var playlistCollection = _userSettings.PlaylistCollection;
            if (playlistCollection != null)
            {
                Playlist retrievedPlaylist = playlistCollection.FirstOrDefault(p => p.PlaylistTitle == playlist.PlaylistTitle);
                if (retrievedPlaylist != null)
                {
                    
                }
            }
        }
    }
}
