using Newtonsoft.Json;

namespace PrismSample2019.Core.Models
{
    public class Search
    {
        public string Title { get; set; }
        public string Year { get; set; }

        [JsonProperty(PropertyName = "imdbID")]
        public string ImdbId { get; set; }

        public string Type { get; set; }
        public string Poster { get; set; }
    }
}