using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using vp.Models;

namespace vp.Extensions
{
    public static class VideoExtensions
    {
        /// <summary>
        /// Gets the length of a <see cref="Video"/>
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public static TimeSpan GetDuration(this Video video)
        {
            MediaFile inputFile = new MediaFile { Filename = video.Path };
            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }
            return inputFile.Metadata.Duration;
        }
    }
}
