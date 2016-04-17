using System.Net;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IWebhooks
    {
        Task<Response> List(object query = null);
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

        public async Task<Response> List(object query = null)
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

            var response = new Response();
            LeftRight.SetValuesToMatch(response, result);
            return response;
        }
    }
}