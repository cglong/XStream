using System.Collections.Generic;
namespace XStream.Phone.Model
{
    public class Track
    {
        public Album album { get; set; }
        public int bitrate { get; set; }
        public int id { get; set; }
        public int length { get; set; }
        public int track_no { get; set; }
        public string slug { get; set; }
        public string Title { get; set; }
        public string URI { get; set; }
        public IEnumerable<string> files { get; set; }
    }
}
