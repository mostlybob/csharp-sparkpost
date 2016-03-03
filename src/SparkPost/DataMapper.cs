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
                ["content"] = transmission.Content.ToDictionary(),
                ["recipients"] = BuildTheRecipientRequestFrom(transmission)
            };
        }

        private static object BuildTheRecipientRequestFrom(Transmission transmission)
        {
            return transmission.ListId != null
                ? (object) new Dictionary<string, object> {["list_id"] = transmission.ListId}
                : transmission.Recipients.Select(x => x.ToDictionary());
        }
    }
}