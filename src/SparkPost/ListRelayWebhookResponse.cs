using System.Collections.Generic;
using Newtonsoft.Json;

namespace SparkPost
{
    public class ListRelayWebhookResponse : Response
    {
        public IEnumerable<RelayWebhook> RelayWebhooks { get; set; }

        public static ListRelayWebhookResponse CreateFromResponse(Response response)
        {
            var result = new ListRelayWebhookResponse();
            LeftRight.SetValuesToMatch(result, response);

            var results = JsonConvert.DeserializeObject<dynamic>(result.Content).results;
            var relayWebhooks = new List<RelayWebhook>();
            foreach(var r in results)
                relayWebhooks.Add(ConvertToARelayWebhook(r));

            result.RelayWebhooks = relayWebhooks;
            return result;
        }

        internal static RelayWebhook ConvertToARelayWebhook(dynamic r)
        {
            var relayWebhook = new RelayWebhook
            {
                Id = r.id,
                Name = r.name,
                Target = r.target,
                AuthToken = r.auth_token,
                Match = new RelayWebhookMatch
                {
                    Protocol = r.match.protocol,
                    Domain = r.match.domain
                }
            };
            return relayWebhook;
        }
    }
}