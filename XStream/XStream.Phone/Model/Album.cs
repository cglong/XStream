using Newtonsoft.Json;

namespace XStream.Phone.Model
{
    public class Album
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cover")]
        public string ImageURL { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
