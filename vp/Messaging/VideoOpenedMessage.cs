using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp.Models;

namespace vp.Messaging
{
    public class VideoOpenedMessage
    {
        public readonly Video Video;

        public VideoOpenedMessage(Video video)
        {
            Video = video;
        }
    }
}
