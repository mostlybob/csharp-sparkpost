using System.Collections.Generic;
using Newtonsoft.Json;

namespace SparkPost
{
    public class ListWebhookResponse : Response
    {
        public IEnumerable<Webhook> Webhooks { get; set; }

        public static ListWebhookResponse CreateFromResponse(Response result)
        {
            var response = new ListWebhookResponse();
            LeftRight.SetValuesToMatch(response, result);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;
            var webhooks = new List<Webhook>();
            foreach(var r in results)
            {
                var events = new List<string>();
                foreach(var i in r.events)
                    events.Add(i.ToString());
                var webhook = new Webhook
                {
                    Id = r.id,
                    Name = r.name,
                    Target = r.target,
                    Events = events,
                    AuthType = r.auth_type,
                    AuthRequestDetails = r.auth_request_details,
                    AuthCredentials = r.auth_credentials,
                    AuthToken = r.auth_token,
                    LastSuccessful = r.last_successful,
                    LastFailure = r.last_failure
                };
                webhooks.Add(webhook);
            }
            response.Webhooks = webhooks;
            return response;
        }
    }
}