using Newtonsoft.Json;

namespace SparkPost
{
    public class PageLink
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("rel")]
        public string Type { get; set; }
    }
}