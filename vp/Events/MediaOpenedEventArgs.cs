using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp.Annotations;
using vp.Models;

namespace vp.Events
{
    public class MediaOpenedEventArgs : EventArgs
    {
        public readonly Video OpenedVideo;

        public MediaOpenedEventArgs([NotNull] Video video)
        {
            if (video == null) throw new ArgumentNullException("Opened video can't be null");
            OpenedVideo = video;
        }
    }
}
