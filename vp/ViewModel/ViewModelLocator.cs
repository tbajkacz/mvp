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

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using vp.Services.Dialogs;
using vp.Services.Playlists;
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
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<ISerializer, JsonSerializer>();
            SimpleIoc.Default.Register<IPlaylistManager, PlaylistManager>();
            SimpleIoc.Default.Register<IPlaylistCollectionManager, PlaylistCollectionManager>();
            SimpleIoc.Default.Register<IFileDialogService, FileDialogService>();

            //if (true)//ViewModelBase.IsInDesignModeStatic
            //{
                SimpleIoc.Default.Register<IUserSettings, MockUserSettings>();
            //}
            //else
            //{
                //SimpleIoc.Default.Register<IUserSettings, UserSettings>();
            //}

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