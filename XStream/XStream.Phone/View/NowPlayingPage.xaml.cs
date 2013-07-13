using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using XStream.Phone.ViewModel;
using XStream.Phone.Model;

namespace XStream.Phone.View
{
    public partial class NowPlayingPage : PhoneApplicationPage
    {
        public NowPlayingPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewModel = DataContext as NowPlayingViewModel;

            viewModel.Artist = new Artist
            {
                Name = NavigationContext.QueryString["artistName"],
            };
            viewModel.Album = new Album
            {
                ImageURL = NavigationContext.QueryString["imageURL"],
            };
            viewModel.Track = new Track
            {
                Title = NavigationContext.QueryString["title"],
            };
        }
    }
}