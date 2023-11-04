using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MovieHub.Data.Models
{
    public class Show
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("language")]
        public string Language { get; set; }
        
        [BsonElement("genres")]
        public string[] Genres { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("runtime")]
        public int? Runtime { get; set; }

        [BsonElement("averageRuntime")]
        public int? AverageRuntime { get; set; }

        [BsonElement("premiered")]
        public string Premiered { get; set; }

        [BsonElement("ended")]
        public string Ended { get; set; }

        [BsonElement("officialSite")]
        public string OfficialSite { get; set; }

        [BsonElement("schedule")]
        public Schedule Schedule { get; set; }

        [BsonElement("rating")]
        public Rating Rating { get; set; }

        [BsonElement("weight")]
        public int? Weight { get; set; }

        [BsonElement("country")]
        public Country Country { get; set; }

        [BsonElement("webChannel")]
        public WebChannel WebChannel { get; set; }

        [BsonElement("dvdCountry")]
        public Country DvdCountry { get; set; }

        [BsonElement("externals")]
        public Externals Externals { get; set; }

        [BsonElement("image")]
        public Image Image { get; set; }

        [BsonElement("summary")]
        public string Summary { get; set; }

        [BsonElement("updated")]
        public int Updated { get; set; }

        [BsonElement("links")]
        public Links Links { get; set; }
    }

    public class Schedule
    {

        [BsonElement("time")]
        public string Time { get; set; }

        [BsonElement("days")]
        public string[] Days { get; set; }
    }

    public class Rating
    {

        [BsonElement("average")]
        public decimal? Average { get; set; }
    }

    public class Network
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("officialSite")]
        public string OfficialSite { get; set; }
    }

    public class Country
    {
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("timeZone")]
        public string TimeZone { get; set; }
    }

    public class Externals
    {

        [BsonElement("tvrage")]
        public int? TvRage { get; set; }

        [BsonElement("thetvdb")]
        public int? TheTvDb { get; set; }

        [BsonElement("imdb")]
        public string Imdb { get; set; }
    }

    public class Image
    {

        [BsonElement("medium")]
        public string Medium { get; set; }

        [BsonElement("original")]
        public string Original { get; set; }
    }

    public class Links
    {

        [BsonElement("self")]
        public Link Self { get; set; }

        [BsonElement("previousEpisode")]
        public Link PreviousEpisode { get; set; }
    }

    public class Link
    {

        [BsonElement("href")]
        public string Href { get; set; }
    }

    public class WebChannel
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("country")]
        public Country Country { get; set; }

        [BsonElement("officialSite")]
        public string OfficialSite { get; set; }
    }
}
