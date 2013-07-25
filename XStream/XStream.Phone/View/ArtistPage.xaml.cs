using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using XStream.Phone.Model;
using XStream.Phone.ViewModel;
using System;
using GalaSoft.MvvmLight.Messaging;
using XStream.Phone.Core;

namespace XStream.Phone.View
{
    public partial class ArtistPage : PhoneApplicationPage
    {
        public ArtistPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            (DataContext as ArtistViewModel).Artist = new Artist
            {
                Name = NavigationContext.QueryString["name"],
                Id = int.Parse(NavigationContext.QueryString["id"]),
            };
        }

        private void logoutMenuItem_Click(object sender, EventArgs e)
        {
            Messenger.Default.Send<LogoutMessage>(new LogoutMessage());
        }
    }
}