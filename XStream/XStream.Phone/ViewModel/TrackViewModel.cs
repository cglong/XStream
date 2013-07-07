﻿using AgFx;
using GalaSoft.MvvmLight;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using XStream.Phone.Core;
using XStream.Phone.Model;



namespace XStream.Phone.ViewModel
{
    public class TrackViewModel : ViewModelBase
    {
        private NavigationService _navigationService;

        public TrackViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string ApplicationTitle
        {
            get
            {
                return Album.Name;
            }
        }

        public string PageTitle
        {
            get
            {
                return "Tracks";
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

        private Album _album;
        private const string AlbumPropertyName = "Album";
        public Album Album
        {
            get
            {
                return _album;
            }
            set
            {
                Set(AlbumPropertyName, ref _album, value);
                Tracks.Clear();
                TracksList tracksList = DataManager.Current.Load<TracksList>(_album.Id, (result) => { Tracks = result.Tracks; }, null);
                tracksList.PropertyChanged += (s, e) => { IsUpdating = (s as TracksList).IsUpdating; };
            }
        }

        private IList<Track> _tracks = new ObservableCollection<Track>();
        private const string TracksPropertyName = "Tracks";
        public IList<Track> Tracks
        {
            get
            {
                GammaSort(_tracks, new GammaSorter());
                return _tracks;
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (_tracks != null)
                {
                    _tracks.Clear();
                    foreach (var trk in value)
                    {
                        _tracks.Add(trk);
                    }
                    RaisePropertyChanged(TracksPropertyName);
                }
            }
        }

        private void GammaSort(IList<Track> _tracks, GammaSorter betaSorter)
        {
            throw new NotImplementedException();
        }
        //Alphabetically sorting - Aditya
        public static void BetaSort(IList<Track> trkList, Comparison<Track> comparison)
        {
            Track[] myTracks = new Track[trkList.Count];
            Array.Sort(myTracks, new GammaSorter());
            // ArrayList.Adapter((IList)list).Sort(new ComparisonComparer<T>(comparison));
        }

        class GammaSorter : IComparer<Track>
        {
            public int Compare(Track X, Track Y)
            {
                return X.Title.CompareTo(Y.Title);
            }
        }


        private Track _selectedTrack;
        private const string SelectedTrackPropertyName = "SelectedTrack";
        private const string audioKey = "audio/mp3";
        public Track SelectedTrack
        {
            get
            {
                return _selectedTrack;
            }
            set
            {
                if (Set(SelectedTrackPropertyName, ref _selectedTrack, value) && value != null)
                {
                	//MediaElement launcher = new MediaElement();
                    MediaPlayerLauncher launcher = new MediaPlayerLauncher();
                    launcher.Media = new Uri(value.Files[audioKey].Substring(0, 28) + value.Files[audioKey].Substring(34, value.Files[audioKey].Length-34), UriKind.Absolute);
                    launcher.Show();
                    //launcher.Play();
					
	    
                }
            }
        }



    }
}
