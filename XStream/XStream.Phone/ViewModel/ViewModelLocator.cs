/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:XStream.Phone"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using XStream.Phone.Core;
using XStream.Phone.Model;

namespace XStream.Phone.ViewModel
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
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<NavigationService>();

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ArtistViewModel>();
            SimpleIoc.Default.Register<TrackViewModel>();
            SimpleIoc.Default.Register<NowPlayingViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ArtistViewModel Artist
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ArtistViewModel>();
            }
        }

        public TrackViewModel Track
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TrackViewModel>();
            }

        }

        public NowPlayingViewModel NowPlaying
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NowPlayingViewModel>();
            }
        }

        public static Uri ArtistPageUri(Artist artist)
        {
            string uri = string.Format("/View/ArtistPage.xaml?name={0}&id={1}", artist.Name, artist.Id);
            return new Uri(uri, UriKind.Relative);
        }

        public static Uri TrackPageUri(Album album)
        {
            string uri = string.Format("/View/TrackPage.xaml?name={0}&id={1}", album.Name, album.Id);
            return new Uri(uri, UriKind.Relative);
        }

        public static Uri NowPlayingPageUri(Album album, Track track)
        {
            string uri = string.Format("/View/NowPlayingPage.xaml?album={0}&track={1}", album.Id, track.TrackNumber);
            return new Uri(uri, UriKind.Relative);
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}