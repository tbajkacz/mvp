using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp.Models;

namespace vp.Services.Playlists
{
    /// <summary>
    /// Used to save and retrieve playlists state
    /// </summary>
    public interface IPlaylistManager
    {
        /// <summary>
        /// Saves the provided <see cref="PlaylistCollection"/>
        /// </summary>
        /// <param name="playlistCollection"></param>
        void SavePlaylists(PlaylistCollection playlistCollection);

        /// <summary>
        /// Retrieves the main <see cref="PlaylistCollection"/>
        /// </summary>
        /// <returns></returns>
        PlaylistCollection GetPlaylists();

        /// <summary>
        /// Updates the provided <see cref="Playlist"/>
        /// </summary>
        /// <param name="playlist"></param>
        void UpdatePlaylist(Playlist playlist);
    }
}
