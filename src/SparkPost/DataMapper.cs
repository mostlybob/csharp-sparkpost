using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace SparkPost
{
    public class DataMapper
    {
        public DataMapper(string version)
        {
            // sticking with v1 for now
        }

        public virtual IDictionary<string, object> ToDictionary(Transmission transmission)
        {
            return RemoveNulls(new Dictionary<string, object>
            {
                ["content"] = ToDictionary(transmission.Content),
                ["campaign_id"] = transmission.CampaignId,
                ["description"] = transmission.Description,
                ["return_path"] = transmission.ReturnPath,
                ["metadata"] = transmission.Metadata.Count > 0 ? transmission.Metadata : null,
                ["options"] = ToDictionary(transmission.Options),
                ["substitution_data"] = transmission.SubstitutionData.Count > 0 ? transmission.SubstitutionData : null,
                ["recipients"] = BuildTheRecipientRequestFrom(transmission)
            });
        }

        public virtual IDictionary<string, object> ToDictionary(Recipient recipient)
        {
            return RemoveNulls(new Dictionary<string, object>
            {
                ["address"] = ToDictionary(recipient.Address),
                ["return_path"] = recipient.ReturnPath,
                ["tags"] = recipient.Tags.Count > 0 ? recipient.Tags : null,
                ["metadata"] = recipient.Metadata.Count > 0 ? recipient.Metadata : null,
                ["substitution_data"] = recipient.SubstitutionData.Count > 0 ? recipient.SubstitutionData : null,
            });
        }

        public virtual IDictionary<string, object> ToDictionary(Address address)
        {
            return RemoveNulls(new Dictionary<string, object>
            {
                ["email"] = address.Email,
                ["name"] = address.Name,
                ["header_to"] = address.HeaderTo,
            });
        }

        public virtual IDictionary<string, object> ToDictionary(Options options)
        {
            return RemoveNulls(new Dictionary<string, object>
            {
                ["click_tracking"] = options.ClickTracking.HasValue && options.ClickTracking.Value ? "true" : "false"
            });
        }

        public virtual IDictionary<string, object> ToDictionary(Content content)
        {
            return RemoveNulls(new Dictionary<string, object>()
            {
                ["from"] = content.From.Email,
                ["subject"] = content.Subject,
                ["text"] = content.Text,
                ["html"] = content.Html,
                ["reply_to"] = content.ReplyTo,
                ["template_id"] = content.TemplateId,
                ["headers"] = content.Headers.Keys.Count > 0 ? content.Headers : null,
            });
        }

        private object BuildTheRecipientRequestFrom(Transmission transmission)
        {
            return transmission.ListId != null
                ? (object) new Dictionary<string, object> {["list_id"] = transmission.ListId}
                : transmission.Recipients.Select(ToDictionary);
        }

        private static IDictionary<string, object> RemoveNulls(IDictionary<string, object> dictionary)
        {
            var newDictionary = new Dictionary<string, object>();
            foreach (var key in dictionary.Keys.Where(k => dictionary[k] != null))
                newDictionary[key] = dictionary[key];
            return newDictionary;
        }
    }
}