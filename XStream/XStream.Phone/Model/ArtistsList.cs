using AgFx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace XStream.Phone.Model
{
    public class ArtistsList : ModelItemBase<LoadContext>
    {
        public ArtistsList()
        {
        }

        public ArtistsList(object id) : base(new LoadContext(id))
        {
        }

        public IList<Artist> Artists { get; set; }

        public class ArtistsListDataLoader : IDataLoader<LoadContext>
        {
            private const string Uri = "http://xstream.cloudapp.net:9002/artists";
            public LoadRequest GetLoadRequest(LoadContext loadContext, Type objectType)
            {
                return new WebLoadRequest(loadContext, new Uri(Uri));
            }

            public object Deserialize(LoadContext loadContext, Type objectType, Stream stream)
            {
                string json;
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject<IList<Artist>>(json);
                return new ArtistsList { Artists = result };
            }
        }
    }
}
