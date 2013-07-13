using AgFx;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XStream.Phone.Core;
using XStream.Phone.Model;

namespace XStream.Phone.ViewModel
{
    /// <summary>
    /// This class contains properties that the artist View can data bind to.
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
    public class ArtistViewModel : ViewModelBase
    {
        private NavigationService _navigationService;

        public ArtistViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string ApplicationTitle
        {
            get
            {
                return Artist.Name;
            }
        }

        public string PageTitle
        {
            get
            {
                return "Albums";
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

        public string LoadingTitle
        {
            get
            {
                return "Loading...";
            }
        }


        private Artist _artist;
        private const string ArtistPropertyName = "Artist";
        public Artist Artist
        {
            get
            {
                return _artist;
            }
            set
            {
                Set(ArtistPropertyName, ref _artist, value);
                Albums.Clear();
                AlbumsList albumsList = DataManager.Current.Load<AlbumsList>(_artist.Id, (result) => { Albums = result.Albums; }, null);
                albumsList.PropertyChanged += (s, e) => { IsLoading = (s as AlbumsList).IsUpdating; };
            }
        }

        private IList<Album> _albums = new ObservableCollection<Album>();
        private const string AlbumsPropertyName = "Albums";
        public IList<Album> Albums
        {
            get
            {
                return _albums;
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (_albums != null)
                {
                    _albums.Clear();
                    foreach (var artist in value)
                    {
                        _albums.Add(artist);
                    }
                    RaisePropertyChanged(AlbumsPropertyName);
                }
            }
        }

        private Album _selectedAlbum;
        private const string SelectedAlbumPropertyName = "SelectedAlbum";
        public Album SelectedAlbum
        {
            get
            {
                return _selectedAlbum;
            }
            set
            {
                if (Set(SelectedAlbumPropertyName, ref _selectedAlbum, value) && value != null)
                {
                    _navigationService.NavigateTo(ViewModelLocator.TrackPageUri(Artist, SelectedAlbum));
                    SelectedAlbum = null;
                }
            }
        }
    }
}