using System;
using System.Threading.Tasks;
using System.Windows;
using vp.Events;
using vp.Models;

namespace vp.Services.Media
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMediaService
    {
        Task Play();

        /// <summary>
        /// Opens the passed <see cref="Video"/> and sets the media position to <see cref="position"/>(if null then position gets set to 0)
        /// </summary>
        /// <param name="video"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        Task Open(Video video, TimeSpan? position = null);

        Task Pause();

        Task Stop();

        bool IsPlaying { get; }

        event RoutedEventHandler MediaEnded;

        event EventHandler<MediaOpenedEventArgs> MediaOpened;
    }
}
