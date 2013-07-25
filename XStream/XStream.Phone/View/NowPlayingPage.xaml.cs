using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using XStream.Phone.ViewModel;
using XStream.Phone.Model;
using System.Windows.Controls;
using System.Windows;

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
                Mp3 = NavigationContext.QueryString["mp3"],
            };
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            pauseButton.Visibility = Visibility.Collapsed;
            playButton.Visibility = Visibility.Visible;
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
            playButton.Visibility = Visibility.Collapsed;
            pauseButton.Visibility = Visibility.Visible;
        }
    }
}