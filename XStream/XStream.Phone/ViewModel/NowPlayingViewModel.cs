using GalaSoft.MvvmLight;
using XStream.Phone.Core;

namespace XStream.Phone.ViewModel
{
    public class NowPlayingViewModel : ViewModelBase
    {
        private NavigationService _navigationService;

        public NowPlayingViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
