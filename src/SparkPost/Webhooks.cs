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
        Task<bool> Delete(string id);
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

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            return ListWebhookResponse.CreateFromResponse(response);
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

        public async Task<bool> Delete(string id)
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/webhooks/{id}",
                Method = "DELETE"
            };

            var response = await requestSender.Send(request);
            return response.StatusCode == HttpStatusCode.NoContent;
        }
    }
}