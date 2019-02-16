using vp.Models;

namespace vp.Services.Settings
{
    /// <summary>
    /// Contains user defined settings and data. Can be used to bind to WPF controls
    /// </summary>
    public interface IUserSettings
    {
        /// <summary>
        /// Saves any changes made to the <see cref="IUserSettings"/> during current session
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Main <see cref="PlaylistCollection"/>, get returns a copy of it
        /// </summary>
        PlaylistCollection PlaylistCollection { get; set; }

        /// <summary>
        /// Current volume level set by the user
        /// </summary>
        double Volume { get; set; }

        /// <summary>
        /// Playlist which was playing during the previous instance of the app null if none
        /// </summary>
        Playlist LastPlayedPlaylist { get; set; }

        /// <summary>
        /// Defines if the right DataGrid with CurrentPlayist was open or not
        /// </summary>
        bool IsOpenCurrentPlaylistPanel { get; set; }
    }
}
