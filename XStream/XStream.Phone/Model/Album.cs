using System.Collections.Generic;
namespace XStream.Phone.Model
{
    public class Album
    {
        public IEnumerable<Artist> Artists { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string URI { get; set; }
        public string Cover { get; set; }
        public int id { get; set; }
        public string slug { get; set; }
    }
}
