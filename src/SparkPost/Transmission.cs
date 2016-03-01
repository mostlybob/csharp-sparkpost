using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.Design;

namespace SparkPost
{
    public class Transmission
    {
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
    }

    public class Content
    {
        public string Html { get; set; }
        public string Text { get; set; }
        public string Subject { get; set; }
        public IList<Email> From { get; set; }
        public string ReplyTo { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
        public IEnumerable<Attachment> InlineImages { get; set; }
        public string TemplateId { get; set; }
        public bool? UseDraftTemplate { get; set; }
    }

    public class Email
    {
        public string Name { get; set; }
        public string Address { get; set; }
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