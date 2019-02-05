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
    public class UserSettings : IUserSettings
    {
        private readonly ISerializer _serializer;

        public PlaylistCollection PlaylistCollection
        {
            get => _serializer.Deserialize<PlaylistCollection>(Properties.UserSettings.Default.PlaylistCollection);
            set => Properties.UserSettings.Default.PlaylistCollection = _serializer.Serialize(value);
        }

        public double Volume
        {
            get => Properties.UserSettings.Default.Volume;
            set => Properties.UserSettings.Default.Volume = value;
        }

        public UserSettings(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void SaveChanges()
        {
            Properties.UserSettings.Default.Save();
        }

        ~UserSettings()
        {
            SaveChanges();
        }
    }
}
