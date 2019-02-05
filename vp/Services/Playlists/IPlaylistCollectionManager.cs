using vp.Models;

namespace vp.Services.Playlists
{
    /// <summary>
    /// Used to manage a <see cref="PlaylistCollection"/>
    /// </summary>
    public interface IPlaylistCollectionManager
    {
        /// <summary>
        /// Current <see cref="PlaylistCollection"/>
        /// </summary>
        PlaylistCollection CurrentCollection { get; set; }

        /// <summary>
        /// Updates the passed <see cref="Playlist"/>
        /// </summary>
        /// <param name="playlist"></param>
        void UpdatePlaylist(Playlist playlist);
    }
}