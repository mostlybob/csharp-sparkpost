using System.Collections.Generic;
using Newtonsoft.Json;

namespace SparkPost
{
    public class RetrieveRecipientListsResponse : Response
    {
        public List<Recipient> RecipientLists { get; set; }

        public string Id { get; set; }

        public string Name { set; get; }

        public string Description { get; set; }

        public Attributes Attributes { get; set; }

        public int TotalAcceptedRecipients { get; set; }

        public static List<Recipient> CreateFromResponse(Response response)
        {
            var result = new List<Recipient>();

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;

            foreach (var r in results.recipients)
                result.Add(ConvertToRecipient(r));

            return result;
        }

        internal static Recipient ConvertToRecipient(dynamic item)
        {
            return new Recipient
            {
                Address = new Address { Email = item.address.email, Name = item.address.name },
                ReturnPath = item.return_path,
                Metadata = ConvertToADictionary(item.metadata),
                SubstitutionData = ConvertToADictionary(item.substitution_data)
            };
        }

        private static dynamic ConvertToADictionary(dynamic @object)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(@object));
        }
    }
}