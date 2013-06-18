namespace XStream.Phone.Model
{
    public class Artist
    {
        public string Name { get; set; }        // The Artist's name. 
        public string ImageURL { get; set; }    // Link to a Photo.
        public string URI { get; set; }         // URI of the resource's instance. 
        public string slug { get; set; }        // Slug field for artist's name.
        public int id { get; set; }             // Instance's SQL PK. 
    }
}
