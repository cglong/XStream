using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStream.Phone.Model
{
    public class Track
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("number")]
        public int TrackNumber { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("files")]
        public IDictionary<string, string> Files { get; set; }

        public string Mp3
        {
            get
            {
                return Files["audio/mp3"];
            }
            set
            {
                if (Files == null)
                {
                    Files = new Dictionary<string, string>();
                }
                Files["audio/mp3"] = value;
            }
        }
    }
}
