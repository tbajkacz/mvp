using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp.Models;

namespace vp.Messaging
{
    public class PlayPlaylistMessage
    {
        public readonly Playlist Playlist;

        public PlayPlaylistMessage(Playlist playlist)
        {
            Playlist = playlist;
        }
    }
}
