using System.Collections.Generic;

namespace SparkPost
{
    public class ListMessageEventsResponse : Response
    {
        public IEnumerable<MessageEvent> MessageEvents { get; set; }

        public IList<PageLink> Links { get; set; }

        public int TotalCount { get; set; }
    }
}