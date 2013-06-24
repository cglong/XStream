using AgFx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace XStream.Phone.Model
{
    public class AlbumsList : ModelItemBase<AlbumsListLoadContext>
    {
        public AlbumsList()
        {
        }

        public AlbumsList(int id) : base(new AlbumsListLoadContext(id))
        {
        }

        public IList<Album> Albums { get; set; }

        public class AlbumsListDataLoader : IDataLoader<AlbumsListLoadContext>
        {
            private const string UriFormat = "http://xstream.cloudapp.net:9002/albums?artist={0}";
            public LoadRequest GetLoadRequest(AlbumsListLoadContext loadContext, Type objectType)
            {
                string uri = String.Format(UriFormat, loadContext.Id);
                return new WebLoadRequest(loadContext, new Uri(uri));
            }

            public object Deserialize(AlbumsListLoadContext loadContext, Type objectType, Stream stream)
            {
                string json;
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject<IList<Album>>(json);
                return new AlbumsList { Albums = result };
            }
        }
    }

    public class AlbumsListLoadContext : LoadContext
    {
        public int Id
        {
            get
            {
                return (int)Identity;
            }
        }

        public AlbumsListLoadContext(int id) : base(id)
        {
        }
    }
}
