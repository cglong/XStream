using AgFx;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using XStream.Phone.Core;
using XStream.Phone.Model;

namespace XStream.Phone.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private NavigationService _navigationService;

        public MainViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            Messenger.Default.Register<LogoutMessage>(this, this.GetType(), Logout);
        }

        public string ApplicationTitle
        {
            get
            {
                return "XStream";
            }
        }

        public string PageTitle
        {
            get
            {
                return "Artists";
            }
        }

        private bool _isLoading;
        private const string IsLoadingPropertyName = "IsLoading";
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                Set(IsLoadingPropertyName, ref _isLoading, value);
            }
        }

        private string _loadingTitle;
        private const string LoadingTitlePropertyName = "LoadingTitle";
        public string LoadingTitle
        {
            get
            {
                return _loadingTitle;
            }
            set
            {
                Set(LoadingTitlePropertyName, ref _loadingTitle, value);
            }
        }

        private IList<Artist> _artists = new ObservableCollection<Artist>();
        private const string ArtistsPropertyName = "Artists";
        public IList<Artist> Artists
        {
            get
            {
                if (_artists.Count == 0)
                {
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("token"))
                    {
                        LoadArtists();
                    }
                    else
                    {
                        Messenger.Default.Send<LoginMessage>(new LoginMessage(Login));
                    }
                }
                return _artists;
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (_artists != null)
                {
                    _artists.Clear();
                    foreach (var artist in value)
                    {
                        _artists.Add(artist);
                    }
                    RaisePropertyChanged(ArtistsPropertyName);
                }
            }
        }

        private Artist _selectedArtist;
        private const string SelectedArtistPropertyName = "SelectedArtist";
        public Artist SelectedArtist
        {
            get
            {
                return _selectedArtist;
            }
            set
            {
                if (Set(SelectedArtistPropertyName, ref _selectedArtist, value) && value != null)
                {
                    _navigationService.NavigateTo(ViewModelLocator.ArtistPageUri(SelectedArtist));
                    SelectedArtist = null;
                }
            }
        }

        private void Login(Telerik.Windows.Controls.InputPromptClosedEventArgs e)
        {
            IDictionary<string, string> info = new Dictionary<string, string>(2);
            info.Add("name", e.Text);
            info.Add("password", e.Text2);

            this.LoadingTitle = "Signing in...";
            var settings = IsolatedStorageSettings.ApplicationSettings;
            User user = DataManager.Current.Load<User>(info, (result) => { settings.Add("token", result.Token); settings.Save(); LoadArtists(); }, null);
            user.PropertyChanged += (s, ev) => { IsLoading = (s as User).IsUpdating; };
        }

        private void LoadArtists()
        {
            this.LoadingTitle = "Loading...";
            ArtistsList artistsList = DataManager.Current.Load<ArtistsList>("", (result) => { Artists = result.Artists; }, null);
            artistsList.PropertyChanged += (s, e) => { IsLoading = (s as ArtistsList).IsUpdating; };
        }

        private void Logout(LogoutMessage message)
        {
            IDictionary<string, string> info = new Dictionary<string, string>(1);
            info.Add("logout", "true");

            var settings = IsolatedStorageSettings.ApplicationSettings;
            DataManager.Current.Load<User>(info, Cleanup<User>, null);
        }

        private void Cleanup<T>(T result)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Remove("token");
            settings.Save();

            DataManager.Current.DeleteCache();
            Artists = new List<Artist>();
        }
    }
}