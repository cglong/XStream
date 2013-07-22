using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using XStream.Phone.Model;

namespace XStream.Phone.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string alphabet = "#abcdefghijklmnopqrstuvwxyz";

        public MainPage()
        {
            InitializeComponent();
            Messenger.Default.Register<DialogMessage>(this, DisplayLoginPrompt);

            // we do not want async balance since item templates are simple
            this.jumpList.IsAsyncBalanceEnabled = false;

            // add custom group picker items, including all alphabetic characters
            List<string> groupPickerItems = new List<string>(32);
            foreach (char c in this.alphabet)
            {
                groupPickerItems.Add(new string(c, 1));
            }
            this.jumpList.GroupPickerItemsSource = groupPickerItems;

            // hook the GroupPickerItemTap event to provide our custom logic since the jump list does not know how to manipulate our items
            this.jumpList.GroupPickerItemTap += this.jumpList_GroupPickerItemTap;

            // add the group and sort descriptors
            int num = 0;
            GenericGroupDescriptor<Artist, string> groupByArtistName = new GenericGroupDescriptor<Artist, string>(artist => int.TryParse(artist.Name.Substring(0, 1).ToLower(), out num) ? "#" : artist.Name.Substring(0, 1).ToLower());
            this.jumpList.GroupDescriptors.Add(groupByArtistName);

            GenericSortDescriptor<Artist, string> sort = new GenericSortDescriptor<Artist, string>(artist => artist.Name);
            this.jumpList.SortDescriptors.Add(sort);
        }

        private void DisplayLoginPrompt(DialogMessage message)
        {
            Style usernameStyle = new Style(typeof(RadTextBox));
            usernameStyle.Setters.Add(new Setter(RadTextBox.HeaderProperty, "username"));
            Style passwordStyle = new Style(typeof(RadPasswordBox));
            passwordStyle.Setters.Add(new Setter(RadPasswordBox.HeaderProperty, "password"));

            InputPromptSettings settings = new InputPromptSettings();
            settings.Field1Mode = InputMode.Text;
            settings.Field1Style = usernameStyle;
            settings.Field2Mode = InputMode.Password;
            settings.Field2Style = passwordStyle;

            string messageTitle = "XStream";
            string messageText = "Please enter your login information:";

            RadInputPrompt.Show(settings, messageTitle, MessageBoxButtons.OKCancel, messageText);
        }

        private void jumpList_GroupPickerItemTap(object sender, GroupPickerItemTapEventArgs e)
        {
            foreach (DataGroup group in this.jumpList.Groups)
            {
                if (object.Equals(e.DataItem, group.Key))
                {
                    e.DataItemToNavigate = group;
                    return;
                }
            }
        }
    }
}