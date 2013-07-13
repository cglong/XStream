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

        public static Uri TrackPageUri(Artist artist, Album album)
        {
            string uri = string.Format("/View/TrackPage.xaml?artistName={0}&name={1}&id={2}&imageURL={3}", artist.Name, album.Name, album.Id, album.ImageURL);
            return new Uri(uri, UriKind.Relative);
        }

        public static Uri NowPlayingPageUri(Artist artist, Album album, Track track)
        {
            string uri = string.Format("/View/NowPlayingPage.xaml?artistName={0}&imageURL={1}&title={2}", artist.Name, album.ImageURL, track.Title);
            return new Uri(uri, UriKind.Relative);
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}