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
            LoadFrom(message);
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

        private void LoadFrom(MailMessage message)
        {
            Content.From = ConvertToAddress(message.From);
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
            
            foreach (var attachment in message.Attachments)
                Content.Attachments
                    .Add(File.Create<Attachment>(attachment.ContentStream, attachment.ContentType.Name));
        }

        private static Address ConvertToAddress(MailAddress address)
        {
            return new Address(address.Address, address.DisplayName);
        }

        private static string GetViewContent(AlternateViewCollection views, string type)
        {
            var view = views.FirstOrDefault(v => v.ContentType.MediaType == type);
            return view == null ? null : GetViewContent(view);
        }

        private static string GetViewContent(AlternateView view)
        {
            var reader = new StreamReader(view.ContentStream);

            if (view.ContentStream.CanSeek)
                view.ContentStream.Position = 0;

            return reader.ReadToEnd();
        }

        private void AddRecipients(MailAddressCollection addresses, RecipientType type)
        {
            foreach(var recipient in ConvertToRecipients(addresses, type))
                Recipients.Add(recipient);
        }

        private static IEnumerable<Recipient> ConvertToRecipients(MailAddressCollection addresses, RecipientType type)
        {
            return addresses.Select(a => ConvertToARecipient(type, a));
        }

        private static Recipient ConvertToARecipient(RecipientType type, MailAddress address)
        {
            return new Recipient()
            {
                Type = type,
                Address = ConvertToAddress(address)
            };
        }
    }
}