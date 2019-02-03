using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace vp.Models
{
    public class PlaylistCollection : ObservableCollection<Playlist>
    {
        public PlaylistCollection() : base()
        {

        }

        public PlaylistCollection(IEnumerable<Playlist> playlistCollection) : base(playlistCollection)
        {

        }
    }
}
