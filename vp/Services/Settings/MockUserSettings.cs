using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new Playlist {PlaylistTitle = "First", Videos = new ObservableCollection<Video>
                {
                    new Video{Name = "First"}
                }},
                new Playlist {PlaylistTitle = "Second", Videos = new ObservableCollection<Video>
                {
                    new Video{Name = "First"},
                    new Video{Name = "Second"},
                    new Video{Name = "Third"}
                }}
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
    }
}
