using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Main <see cref="PlaylistCollection"/> of the application
        /// </summary>
        PlaylistCollection PlaylistCollection { get; set; }

        /// <summary>
        /// Current volume level set by the user
        /// </summary>
        double Volume { get; set; }
    }
}
