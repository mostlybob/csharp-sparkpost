using System.Collections.Generic;

namespace SparkPost
{
    public class Transmission
    {
        public Transmission()
        {
            Recipients = new List<Recipient>();
            Metadata = new Dictionary<string, string>();
            SubstitutionData = new Dictionary<string, string>();
            Content = new Content();
        }

        public string Id { get; set; }
        public string State { get; set; }
        public Options Options { get; set; }

        // it’s either `recipients: []` or `recipients: { list_id: 'theID' }`
        public IList<Recipient> Recipients { get; set; }
        public string ListId { get; set; }

        public string CampaignId { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public IDictionary<string, string> SubstitutionData { get; set; }
        public string ReturnPath { get; set; }
        public Content Content { get; set; }
        public int TotalRecipients { get; set; }
        public int NumGenerated { get; set; }
        public int NumFailedGeneration { get; set; }
        public int NuMInvalidRecipients { get; set; }

        public virtual IDictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["content"] = Content.ToDictionary()
            };
        }
    }
}