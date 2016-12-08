using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Linq;
using System;
using System.IO;

namespace SparkPost
{
    public class Transmission
    {
        public Transmission()
        {
            Recipients = new List<Recipient>();
            Metadata = new Dictionary<string, object>();
            SubstitutionData = new Dictionary<string, object>();
            Content = new Content();
            Options = new Options();            
        }

        public Transmission(MailMessage message): this()
        {
            Parse(message);
        }

        public string Id { get; set; }
        public string State { get; set; }
        public Options Options { get; set; }

        public IList<Recipient> Recipients { get; set; }
        public string ListId { get; set; }

        public string CampaignId { get; set; }
        public string Description { get; set; }
        public IDictionary<string, object> Metadata { get; set; }
        public IDictionary<string, object> SubstitutionData { get; set; }
        public string ReturnPath { get; set; }
        public Content Content { get; set; }
        public int TotalRecipients { get; set; }
        public int NumGenerated { get; set; }
        public int NumFailedGeneration { get; set; }
        public int NumInvalidRecipients { get; set; }

        public void Parse(MailMessage message)
        {
            Content.From = new Address(message.From.Address, message.From.DisplayName);
            Content.Subject = message.Subject;

            AddRecipients(message.To, RecipientType.To);
            AddRecipients(message.CC, RecipientType.CC);
            AddRecipients(message.Bcc, RecipientType.BCC);

            if (message.ReplyToList.Any())
                Content.ReplyTo = message.ReplyToList.First().Address;

            if (message.IsBodyHtml)
                Content.Html = message.Body;
            else
                Content.Text = message.Body;

            var textTypes = new[] { MediaTypeNames.Text.Plain, MediaTypeNames.Text.Html };
            var views = message.AlternateViews;
            if (views.Any() && views.Count <= 2 &&
                !views.Select(av => av.ContentType.MediaType).Except(textTypes).Any())
            {
                var text = GetViewContent(views, MediaTypeNames.Text.Plain);
                if (text != null)
                    Content.Text = text;

                var html = GetViewContent(views, MediaTypeNames.Text.Html);
                if (html != null)
                    Content.Html = html;
            }
            
            foreach (var attach in message.Attachments)
            {
                var newAttach = File.Create<Attachment>(attach.ContentStream, attach.ContentType.Name);
                Content.Attachments.Add(newAttach);
            }
        }

        private string GetViewContent(AlternateViewCollection views, string type)
        {
            string result = null;

            var view = views.FirstOrDefault(v => v.ContentType.MediaType == type);
            if (view != null)
            { 
                var rdr = new StreamReader(view.ContentStream);
                if (view.ContentStream.CanSeek)
                    view.ContentStream.Position = 0;
                result = rdr.ReadToEnd();
            }

            return result;
        }

        private void AddRecipients(MailAddressCollection addrs, RecipientType type)
        {
            foreach (var addr in addrs)
            {
                var recp = new Recipient()
                {
                    Type = type,
                    Address = new Address(addr.Address, addr.DisplayName)
                };
                Recipients.Add(recp);
            }
        }
    }
}