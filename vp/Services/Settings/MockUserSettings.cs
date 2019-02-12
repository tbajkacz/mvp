using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using vp.Models;

namespace vp.Services.Settings
{
    public class MockUserSettings : IUserSettings
    {
        public void SaveChanges()
        {
        }

        public PlaylistCollection PlaylistCollection
        {
            get => new PlaylistCollection
            {
            #pragma warning disable 0618
                new Playlist {PlaylistTitle = "First", Videos = new BindingList<Video>
                {
                    new Video{Name = "First"}
                }},
                new Playlist {PlaylistTitle = "Second", Videos = new BindingList<Video>
                {
                    new Video{Name = "First"},
                    new Video{Name = "Second"},
                    new Video{Name = "Third"}
                }}
            #pragma warning restore 0618
            };
            set
            {

            }
        }

        public double Volume
        {
            get => 0.1;
            set
            {
            }
        }

        public Playlist LastPlayedPlaylist
        {
            get => new Playlist
            {
                PlaylistTitle = "First",
                Videos = new BindingList<Video>
                {
                    #pragma warning disable 0618
                    new Video {Name = "First", TimeWatched = TimeSpan.FromSeconds(20), Length = TimeSpan.FromSeconds(100)}
                    #pragma warning restore 0618
                }
            };
            set { }
        }

        public bool IsOpenCurrentPlaylistPanel
        {
            get => true;
            set { }
        }
    }
}
