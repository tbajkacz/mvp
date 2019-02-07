using System;
using vp.Models;
using vp.Services.Serialization;

namespace vp.Services.Settings
{
    public class UserSettings : IUserSettings
    {
        private readonly ISerializer _serializer;

        public PlaylistCollection PlaylistCollection
        {
            get => _serializer.Deserialize<PlaylistCollection>(Properties.UserSettings.Default.PlaylistCollection) ?? new PlaylistCollection();
            set
            {
                if (value == null) throw new ArgumentNullException($"Null value argument passed to {nameof(IUserSettings.PlaylistCollection)}");
                Properties.UserSettings.Default.PlaylistCollection = _serializer.Serialize(value);
            }
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
