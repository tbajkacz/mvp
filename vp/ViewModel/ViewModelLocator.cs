/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:vp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MahApps.Metro.Controls.Dialogs;
using vp.Services.Dialogs;
using vp.Services.Navigation;
using vp.Services.Serialization;
using vp.Services.Settings;

namespace vp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<ISerializer, JsonSerializer>();
            SimpleIoc.Default.Register<IFileDialogService, FileDialogService>();
            SimpleIoc.Default.Register<IPageNavigationService, PageNavigationService>();

            

            SimpleIoc.Default.GetInstance<IPageNavigationService>().Register(PageKeys.MediaPage, new Uri("../Pages/MediaPage.xaml", UriKind.Relative), true);
            SimpleIoc.Default.GetInstance<IPageNavigationService>().Register(PageKeys.PlaylistsPage, new Uri("../Pages/PlaylistsPage.xaml", UriKind.Relative));

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IUserSettings, MockUserSettings>();
            }
            else
            {
                //Visual studio designer has a problem with IDialogCoordinator
                SimpleIoc.Default.Register<IDialogCoordinator>(() => DialogCoordinator.Instance);
                SimpleIoc.Default.Register<IUserSettings, UserSettings>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MediaViewModel>();
            SimpleIoc.Default.Register<PlaylistsViewModel>();
        }

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public MediaViewModel MediaViewModel => SimpleIoc.Default.GetInstance<MediaViewModel>();

        public PlaylistsViewModel PlaylistsViewModel => SimpleIoc.Default.GetInstance<PlaylistsViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}