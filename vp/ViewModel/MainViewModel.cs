using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using vp.Messaging;
using vp.Models;
using vp.Services.Navigation;
using vp.Services.Settings;

namespace vp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPageNavigationService _pageNavigationService;
        private bool _isFullscreen;
        private bool _isTitleBarVisible;
        private bool _isWindowStyleNone;
        private bool _ignoreTaskBarOnFullscreen;
        private string _windowTitle;
        private bool _isBackButtonVisible;

        public bool IsFullscreen
        {
            get => _isFullscreen;
            set => Set(() => IsFullscreen, ref _isFullscreen, value);
        }

        public bool IsTitleBarVisible
        {
            get => _isTitleBarVisible;
            set => Set(() => IsTitleBarVisible, ref _isTitleBarVisible, value);
        }

        public bool IsWindowStyleNone
        {
            get => _isWindowStyleNone;
            set => Set(() => IsWindowStyleNone, ref _isWindowStyleNone, value);
        }

        public bool IgnoreTaskBarOnFullscreen
        {
            get => _ignoreTaskBarOnFullscreen;
            set => Set(() => IgnoreTaskBarOnFullscreen, ref _ignoreTaskBarOnFullscreen, value);
        }

        public bool IsBackButtonVisible
        {
            get => _isBackButtonVisible;
            set => Set(() => IsBackButtonVisible, ref _isBackButtonVisible, value);
        }

        public string WindowTitle
        {
            get => _windowTitle;
            set => Set(() => WindowTitle, ref _windowTitle, value);
        }

        public RelayCommand PreloadPagesCommand { get; }

        public RelayCommand NavigateToStartupPageCommand { get; }

        public MainViewModel(IPageNavigationService pageNavigationService)
        {
            this._pageNavigationService = pageNavigationService;

            _pageNavigationService.Navigated += (s, e) => {
                if (e.Key != PageKeys.DefaultPage)
                {
                    IsBackButtonVisible = true;
                }
                else
                {
                    IsBackButtonVisible = false;
                }
            };

            PreloadPagesCommand = new RelayCommand(OnPreloadPages);
            NavigateToStartupPageCommand = new RelayCommand(OnNavigateToStartupPage);

            RegisterMessages();
            IsFullscreen = false;
            IsTitleBarVisible = true;
            IsWindowStyleNone = false;
            IgnoreTaskBarOnFullscreen = false;
            WindowTitle = "vp";
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<ToggleWindowFullscreenMessage>(this, msg =>
            {
                var temp = Application.Current.MainWindow;
                if (msg.Fullscreen == null)
                {
                    SetFullscreen(!IsFullscreen);
                }
                else
                {
                    SetFullscreen(msg.Fullscreen.Value);
                }
            });

            Messenger.Default.Register<VideoOpenedMessage>(this, msg => WindowTitle = msg.Video.Name);
        }

        private void SetFullscreen(bool fullscreen)
        {
            IsFullscreen = fullscreen;
            IsTitleBarVisible = !fullscreen;
            IsWindowStyleNone = fullscreen;
            IgnoreTaskBarOnFullscreen = fullscreen;
        }

        private void OnNavigateToStartupPage()
        {
            _pageNavigationService.NavigateTo(PageKeys.DefaultPage);
        }

        private void OnPreloadPages()
        {
            _pageNavigationService.LoadSingleInstancePages();
        }
    }
}