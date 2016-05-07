using Newtonsoft.Json;
using System.Collections.Generic;

namespace SparkPost
{
    public class ListMessageEventsResponse : Response
    {
        [JsonProperty("results")]
        public IEnumerable<MessageEvent> MessageEvents { get; set; }

        [JsonProperty("links")]
        public IList<PageLink> Links { get; private set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; private set; }
    }
}