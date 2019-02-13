using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using vp.Messaging;
using vp.vpWindows;
using vp.ViewModel;

namespace vp.Services.Navigation
{
    public class PageNavigationService : IPageNavigationService
    {
        private readonly Dictionary<string, (Uri uri, bool singleInstance)> _pagesUri;
        private readonly Dictionary<string, Page> _pages;


        public PageNavigationService()
        {
            _pagesUri = new Dictionary<string, (Uri uri, bool singleInstance)>();
            _pages = new Dictionary<string, Page>();
        }

        public void Register(string key, Uri uri, bool singleInstance = false)
        {
            if (key == null || uri == null) throw new ArgumentNullException($"Can't register null page");

            if (_pagesUri.ContainsKey(key))
            {
                _pagesUri[key] = (uri, singleInstance);
            }
            else
            {
                _pagesUri.Add(key, (uri, singleInstance));
            }
        }

        public void NavigateTo(string key)
        {
            if (key == null) throw new ArgumentNullException($"Key can't be null");
            if (!_pagesUri.ContainsKey(key)) throw new ArgumentException($"Page {key} does not exist");

            var navigation = ((MainWindow)Application.Current.MainWindow).MainWindowFrame.NavigationService;

            if (!_pagesUri[key].singleInstance)
            {
                navigation.Navigate(_pagesUri[key].uri);
                return;
            }
            if (!_pages.ContainsKey(key))
            {
                Page p = Application.LoadComponent(_pagesUri[key].uri) as Page;
                _pages.Add(key, p);
                navigation.Navigate(p);
            }
            else
            {
                navigation.Navigate(_pages[key]);
            }
        }

        public void LoadSingleInstancePages()
        {
            foreach (var kvp in _pagesUri)
            {
                if (!_pages.ContainsKey(kvp.Key) && _pagesUri[kvp.Key].singleInstance)
                {
                    Page p = Application.LoadComponent(kvp.Value.uri) as Page;
                    _pages.Add(kvp.Key, p);
                }
            }
        }
    }
}
