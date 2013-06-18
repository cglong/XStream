using System.Collections.Generic;

namespace XStream.Phone.Model
{
    public class Track
    {
        public Album Album { get; set; }
        public int Bitrate { get; set; }
        public int Id { get; set; }
        public int Length { get; set; }
        public int Track { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}
