using System;
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
            //TODO REPLACE THIS - really slow
            MediaFile inputFile = new MediaFile { Filename = video.Path };
            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }
            return inputFile.Metadata.Duration;
        }
    }
}
