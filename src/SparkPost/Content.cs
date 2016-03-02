using System.Collections.Generic;

namespace SparkPost
{
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

        public virtual IDictionary<string, object> ToDictionary()
        {
            throw new System.NotImplementedException();
        }
    }
}