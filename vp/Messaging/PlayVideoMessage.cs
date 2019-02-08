using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp.Models;

namespace vp.Messaging
{
    public class PlayVideoMessage
    {
        public readonly Playlist Playlist;

        public readonly Video Video;

        public PlayVideoMessage(Playlist playlist, Video video)
        {
            Playlist = playlist;
            Video = video;
        }
    }
}
