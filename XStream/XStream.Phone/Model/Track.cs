using Newtonsoft.Json;

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

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
