﻿using AgFx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace XStream.Phone.Model
{
    public class TracksList : ModelItemBase<TracksListLoadContext>
    {
        public TracksList()
        {
        }

        public TracksList(int id) : base(new TracksListLoadContext(id))
        {
        }

        public IList<Track> Tracks { get; set; }

        public class TracksListDataLoader : IDataLoader<TracksListLoadContext>
        {
            private const string UriFormat = "http://xstream.cloudapp.net:9002/tracks?album={0}";
            public LoadRequest GetLoadRequest(TracksListLoadContext loadContext, Type objectType)
            {
                string uri = String.Format(UriFormat, loadContext.Id);
                return new WebLoadRequest(loadContext, new Uri(uri));
            }

            public object Deserialize(TracksListLoadContext loadContext, Type objectType, Stream stream)
            {
                string json;
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject<IList<Track>>(json);
                return new TracksList { Tracks = result };
            }
        }


        
    }

    public class TracksListLoadContext : LoadContext
    {
        public int Id
        {
            get
            {
                return (int)Identity;
            }
        }

        public TracksListLoadContext(int id)
            : base(id)
        {
        }
    }

}