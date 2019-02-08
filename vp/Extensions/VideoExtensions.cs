using System;
using System.IO;
using Unosquare.FFME;
using vp.Models;
using Unosquare.FFME.Shared;
namespace vp.Extensions
{
    public static class VideoExtensions
    {
        static VideoExtensions()
        {
            MediaEngine.LoadFFmpeg();
        }
        /// <summary>
        /// Gets the length of a <see cref="Video"/>
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public static TimeSpan GetDuration(this Video video)
        {
            MediaInfo info = MediaEngine.RetrieveMediaInfo(video.Path);
            return info.Duration;
        }
    }
}
