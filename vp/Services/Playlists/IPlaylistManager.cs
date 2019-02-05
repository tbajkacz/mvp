using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp.Models;

namespace vp.Services.Playlists
{
    /// <summary>
    /// Used to manage a single <see cref="Playlist"/>
    /// </summary>
    public interface IPlaylistManager : INotifyPropertyChanged
    {
        /// <summary>
        /// Currently managed <see cref="Playlist"/>, new if none specified
        /// </summary>
        Playlist CurrentPlaylist { get; set; }

        /// <summary>
        /// Contains current <see cref="Video"/>, null if not available
        /// </summary>
        Video CurrentVideo { get; }

        /// <summary>
        /// Sets <see cref="CurrentVideo"/> and returns the next <see cref="Video"/>, null if not available
        /// </summary>
        /// <returns></returns>
        Video Next();

        /// <summary>
        /// Sets <see cref="CurrentVideo"/> and returns the previous <see cref="Video"/>, null if not available
        /// </summary>
        /// <returns></returns>
        Video Prev();
    }
}
