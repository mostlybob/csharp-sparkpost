using System.Collections.Generic;

namespace SparkPost
{
    public class ListWebhookResponse : Response
    {
        public IEnumerable<Webhook> Webhooks { get; set; }
    }
}