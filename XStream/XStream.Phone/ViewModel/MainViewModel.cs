using AgFx;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            ArtistsList artistsList = DataManager.Current.Load<ArtistsList>("", (result) => { Artists = result.Artists; }, null);
            artistsList.PropertyChanged += (s, e) => { IsUpdating = (s as ArtistsList).IsUpdating; };
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

        private bool _isUpdating;
        private const string IsUpdatingPropertyName = "IsUpdating";
        public bool IsUpdating
        {
            get
            {
                return _isUpdating;
            }
            set
            {
                Set(IsUpdatingPropertyName, ref _isUpdating, value);
            }
        }

        public string UpdatingTitle
        {
            get
            {
                return "Updating...";
            }
        }

        private IList<Artist> _artists = new ObservableCollection<Artist>();
        private const string ArtistsPropertyName = "Artists";
        public IList<Artist> Artists
        {
            get
            {
                AlphaSort(_artists, new AlphaSorter());
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

        private void AlphaSort(IList<Artist> _artists, AlphaSorter alphaSorter)
        {
            throw new NotImplementedException();
        }
        //Alphabetically sorting - Aditya
        public static void AlphaSort(IList<Artist> artList, Comparison<Artist> comparison)
        {
            Artist[] myArtists = new Artist[artList.Count];
            Array.Sort(myArtists, new AlphaSorter());
            // ArrayList.Adapter((IList)list).Sort(new ComparisonComparer<T>(comparison));
        }

        class AlphaSorter : IComparer<Artist>
        {
            public int Compare(Artist X, Artist Y)
            {
                return X.Name.CompareTo(Y.Name);
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
                    _navigationService.NavigateTo(ViewModelLocator.ArtistPageUri(_selectedArtist));
                    SelectedArtist = null;
                }
            }
        }
    }
}