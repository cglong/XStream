using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using XStream.Phone.ViewModel;
using XStream.Phone.Model;

namespace XStream.Phone.View
{
    public partial class TrackPage : PhoneApplicationPage
    {
        public TrackPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewModel = DataContext as TrackViewModel;

            viewModel.Artist = new Artist
            {
                Name = NavigationContext.QueryString["artistName"],
            };
            viewModel.Album = new Album
            {
                Name = NavigationContext.QueryString["name"],
                Id = int.Parse(NavigationContext.QueryString["id"]),
                ImageURL = NavigationContext.QueryString["imageURL"],
            };
        }

    }
}