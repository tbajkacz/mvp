using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using vp.Models;
using vp.Services.Serialization;

namespace vp.Services.Settings
{
    public class UserSettings : ObservableObject, IUserSettings
    {
        private readonly ISerializer _serializer;
        private PlaylistCollection _playlistCollection;

        public PlaylistCollection PlaylistCollection
        {
            get => _playlistCollection;
            set { Set(() => PlaylistCollection, ref _playlistCollection, value); }
        }

        public UserSettings(ISerializer serializer)
        {
            _serializer = serializer;
            InitializeProperties();
        }

        /// <summary>
        /// Initializes private backing fields of each property
        /// </summary>
        private void InitializeProperties()
        {
            _playlistCollection = _serializer.Deserialize<PlaylistCollection>(Properties.UserSettings.Default.PlaylistCollection);
        }

        public void SaveChanges()
        {
            Properties.UserSettings.Default.PlaylistCollection = _serializer.Serialize<PlaylistCollection>(PlaylistCollection);
            Properties.UserSettings.Default.Save();
        }
    }
}
