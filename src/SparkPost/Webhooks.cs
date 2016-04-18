using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost
{
    public interface IWebhooks
    {
        Task<ListWebhookResponse> List(object query = null);
        Task<Response> Create(Webhook webhook);
    }

    public class Webhooks : IWebhooks
    {
        private readonly IClient client;
        private readonly IRequestSender requestSender;
        private readonly IDataMapper dataMapper;

        public Webhooks(IClient client, IRequestSender requestSender, IDataMapper dataMapper)
        {
            this.client = client;
            this.requestSender = requestSender;
            this.dataMapper = dataMapper;
        }

        public async Task<ListWebhookResponse> List(object query = null)
        {
            if (query == null) query = new {};
            var request = new Request
            {
                Url = $"/api/{client.Version}/webhooks",
                Method = "GET",
                Data = query
            };

            var result = await requestSender.Send(request);
            if (result.StatusCode != HttpStatusCode.OK) throw new ResponseException(result);

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
                };
                webhooks.Add(webhook);
            }
            response.Webhooks = webhooks;

            return response;
        }

        public async Task<Response> Create(Webhook webhook)
        {
            var request = new Request
            {
                Url = $"api/{client.Version}/webhooks",
                Method = "POST",
                Data = dataMapper.ToDictionary(webhook)
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var updateSuppressionResponse = new Response();
            LeftRight.SetValuesToMatch(updateSuppressionResponse, response);
            return updateSuppressionResponse;
        }
    }
}