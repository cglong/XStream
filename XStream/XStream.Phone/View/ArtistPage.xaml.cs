using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using XStream.Phone.Model;
using XStream.Phone.ViewModel;

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
    }
}