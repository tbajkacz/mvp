using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using vp.Events;

namespace vp.Services.Navigation
{
    /// <summary>
    /// Service used to navigate between pages
    /// </summary>
    public interface IPageNavigationService
    {
        /// <summary>
        /// Registers a page pointed to by the uri argument
        /// </summary>
        /// <param name="key"></param>
        /// <param name="uri"></param>
        /// <param name="singleInstance"></param>
        void Register(string key, Uri uri, bool singleInstance = false);

        /// <summary>
        /// Navigates to a page corresponding to the key argument
        /// </summary>
        /// <param name="key"></param>
        void NavigateTo(string key);

        /// <summary>
        /// Preloads all registered pages into memory
        /// </summary>
        void LoadSingleInstancePages();

        /// <summary>
        /// Event invoked when NavigateTo method completes
        /// </summary>
        event EventHandler<NavigatedEventArgs> Navigated;
    }
}
