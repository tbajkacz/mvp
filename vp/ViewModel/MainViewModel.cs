using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using vp.Models;
using vp.Services.AppService;
using vp.Services.Navigation;
using vp.Services.Settings;

namespace vp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPageNavigationService _pageNavigationService;

        public RelayCommand PreloadPagesCommand { get; }

        public RelayCommand NavigateToStartupPageCommand { get; }

        public MainViewModel(IPageNavigationService pageNavigationService)
        {
            this._pageNavigationService = pageNavigationService;

            PreloadPagesCommand = new RelayCommand(OnPreloadPages);
            NavigateToStartupPageCommand = new RelayCommand(OnNavigateToStartupPage);
        }

        private void OnNavigateToStartupPage()
        {
            _pageNavigationService.NavigateTo("MediaPage");
        }

        private void OnPreloadPages()
        {
            _pageNavigationService.LoadSingleInstancePages();
        }
    }
}