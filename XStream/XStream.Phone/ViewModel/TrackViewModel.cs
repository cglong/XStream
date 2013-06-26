using AgFx;
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
                DataManager.Current.Load<TracksList>(_album.Id, (result) => { Tracks = result.Tracks; }, null);
            }
        }

        private IList<Track> _tracks = new ObservableCollection<Track>();
        private const string TracksPropertyName = "Tracks";
        public IList<Track> Tracks
        {
            get
            {
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
