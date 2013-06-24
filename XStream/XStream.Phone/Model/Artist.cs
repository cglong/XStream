using Newtonsoft.Json;

namespace XStream.Phone.Model
{
    public class Artist
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string ImageURL { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; } 
    }
}
