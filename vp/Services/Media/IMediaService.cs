using System.Threading.Tasks;
using vp.Models;

namespace vp.Services.Media
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMediaService
    {
        Task Play();

        Task Open(Video video);

        Task Pause();

        Task Stop();

        bool IsPlaying { get; }
    }
}
