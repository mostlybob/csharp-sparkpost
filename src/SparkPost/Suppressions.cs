using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost
{
    public class Suppressions
    {
        private readonly Client client;
        private readonly RequestSender requestSender;
        private readonly DataMapper dataMapper;

        public Suppressions(Client client, RequestSender requestSender, DataMapper dataMapper)
        {
            this.client = client;
            this.requestSender = requestSender;
            this.dataMapper = dataMapper;
        }

        public async Task<Response> CreateOrUpdate(IEnumerable<Suppression> suppressions)
        {
            var request = new Request
            {
                Url = $"api/{client.Version}/suppression-list",
                Method = "PUT",
                Data = new { recipients = new [] { new { email = "x@y.com", transactional = true, non_transactional = true, description = "testing"} } },
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;
            return new Response()
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
            };
        }

    }
}