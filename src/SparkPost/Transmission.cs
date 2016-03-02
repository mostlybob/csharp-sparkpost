using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.Design;
using Newtonsoft.Json;

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
    }

    public class Recipient
    {
        public Address Address { get; set; }
        public string ReturnPath { get; set; }
        public IList Tags { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public IDictionary<string, string> SubstitutionData { get; set; }
    }

    public class Content
    {
        public string Html { get; set; }
        public string Text { get; set; }
        public string Subject { get; set; }
        public Address From { get; set; }
        public string ReplyTo { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
        public IEnumerable<InlineImage> InlineImages { get; set; }
        public string TemplateId { get; set; }
        public bool? UseDraftTemplate { get; set; }
    }

    public class Address
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string HeaderTo { get; set; }
    }

    public class Attachment : File
    {
    }

    public class InlineImage : File
    {
    }

    public abstract class File
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }

    public class Options
    {
        public DateTime? StartTime { get; set; }
        public bool? OpenTracking { get; set; }
        public bool? ClickTracking { get; set; }
        public bool? Transactional { get; set; }
        public bool? Sandbox { get; set; }
        public bool? SkipSuppression { get; set; }
    }
}