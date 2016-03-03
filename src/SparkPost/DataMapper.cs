using System.Collections.Generic;
using System.Linq;

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
            return new Dictionary<string, object>
            {
                ["content"] = ToDictionary(transmission.Content),
                ["recipients"] = BuildTheRecipientRequestFrom(transmission)
            };
        }

        public virtual IDictionary<string, object> ToDictionary(Recipient recipient)
        {
            return new Dictionary<string, object>
            {
                ["address"] = recipient.Address.Email
            };
        }

        public virtual IDictionary<string, object> ToDictionary(Content content)
        {
            return new Dictionary<string, object>()
            {
                ["from"] = content.From.Email,
                ["subject"] = content.Subject,
                ["text"] = content.Text
            };
        }

        private object BuildTheRecipientRequestFrom(Transmission transmission)
        {
            return transmission.ListId != null
                ? (object) new Dictionary<string, object> {["list_id"] = transmission.ListId}
                : transmission.Recipients.Select(ToDictionary);
        }
    }
}