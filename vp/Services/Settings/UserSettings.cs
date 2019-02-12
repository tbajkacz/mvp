using System;
using System.Linq;
using vp.Models;
using vp.Services.Serialization;

namespace vp.Services.Settings
{
    public class UserSettings : IUserSettings
    {
        private readonly ISerializer _serializer;
        private PlaylistCollection _playlistCollection;

        public PlaylistCollection PlaylistCollection
        {
            get => _playlistCollection;
            set
            {
                if (value == null) throw new ArgumentNullException($"Null value argument passed to {nameof(IUserSettings.PlaylistCollection)}");
                _playlistCollection = value;
                Properties.UserSettings.Default.PlaylistCollection = _serializer.Serialize(value);
            }
        }

        public double Volume
        {
            get { return Properties.UserSettings.Default.Volume;}
            set => Properties.UserSettings.Default.Volume = value;
        }

        public Playlist LastPlayedPlaylist
        {
            get
            {
                return PlaylistCollection.FirstOrDefault(p =>
                           p.PlaylistTitle == _serializer
                               .Deserialize<Playlist>(Properties.UserSettings.Default.LastPlayedPlaylist)
                               .PlaylistTitle) ?? new Playlist();
            }
            set => Properties.UserSettings.Default.LastPlayedPlaylist = _serializer.Serialize(value);
        }

        public bool IsOpenCurrentPlaylistPanel
        {
            get => Properties.UserSettings.Default.IsOpenCurrentPlaylistPanel;
            set => Properties.UserSettings.Default.IsOpenCurrentPlaylistPanel = value;
        }

        public UserSettings(ISerializer serializer)
        {
            _serializer = serializer;
            _playlistCollection = _serializer.Deserialize<PlaylistCollection>(Properties.UserSettings.Default.PlaylistCollection) ?? new PlaylistCollection();
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
