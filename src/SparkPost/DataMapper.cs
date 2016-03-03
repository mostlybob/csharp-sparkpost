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
                ["recipients"] = BuildTheRecipientRequestFrom(transmission)
            });
        }

        public virtual IDictionary<string, object> ToDictionary(Recipient recipient)
        {
            return RemoveNulls(new Dictionary<string, object>
            {
                ["address"] = recipient.Address.Email
            });
        }

        public virtual IDictionary<string, object> ToDictionary(Content content)
        {
            return RemoveNulls(new Dictionary<string, object>()
            {
                ["from"] = content.From.Email,
                ["subject"] = content.Subject,
                ["text"] = content.Text,
                ["template_id"] = content.TemplateId
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