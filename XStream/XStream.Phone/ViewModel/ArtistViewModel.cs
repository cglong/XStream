﻿using GalaSoft.MvvmLight;
using System.Collections.Generic;
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
            }
        }

        public IList<Album> Albums { get; set; }

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
                }
            }
        }
    }
}