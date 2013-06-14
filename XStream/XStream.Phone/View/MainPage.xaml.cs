using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using System;
using System.Text;
using XStream.Phone.Model;

namespace XStream.Phone.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Artist>(this, (artist) => ReceiveMessage(artist));
        }

        private void ReceiveMessage(Artist artist)
        {
            string uri = string.Format("/View/ArtistPage.xaml?name={0}", artist.Name);
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }
    }
}