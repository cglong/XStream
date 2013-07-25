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
            IDictionary<string, string> login = new Dictionary<string, string>(2);
            login.Add("name", e.Text);
            login.Add("password", e.Text2);

            this.LoadingTitle = "Signing in...";
            var settings = IsolatedStorageSettings.ApplicationSettings;
            User user = DataManager.Current.Load<User>(login, (result) => { settings.Add("token", result.Token); settings.Save(); LoadArtists(); }, null);
            user.PropertyChanged += (s, ev) => { IsLoading = (s as User).IsUpdating; };
        }

        private void LoadArtists()
        {
            this.LoadingTitle = "Loading...";
            ArtistsList artistsList = DataManager.Current.Load<ArtistsList>("", (result) => { Artists = result.Artists; }, null);
            artistsList.PropertyChanged += (s, e) => { IsLoading = (s as ArtistsList).IsUpdating; };
        }
    }
}