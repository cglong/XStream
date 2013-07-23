using GalaSoft.MvvmLight;
using XStream.Phone.Core;
using XStream.Phone.Model;

namespace XStream.Phone.ViewModel
{
    public class NowPlayingViewModel : ViewModelBase
    {
        private NavigationService _navigationService;

        public NowPlayingViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public Artist Artist { get; set; }

        public string ArtistName
        {
            get
            {
                return Artist.Name;
            }
        }

        public Album Album { get; set; }

        public string ImageURL
        {
            get
            {
                return Album.ImageURL;
            }
        }

        public Track Track { get; set; }

        public string Title
        {
            get
            {
                return Track.Title;
            }
        }

        public string Mp3
        {
            get
            {
                return Track.Mp3;
            }
        }
    }
}
