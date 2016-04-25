using System;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IRequestSender
    {
        Task<Response> Send(Request request);
    }

    public class RequestSender : IRequestSender
    {
        private readonly Client client;
        private readonly RequestMethodFinder requestMethodFinder;

        public RequestSender(Client client)
        {
            this.client = client;
            requestMethodFinder = new RequestMethodFinder();
        }

        public async Task<Response> Send(Request request)
        {
            using (var c = client.CustomSettings.CreateANewHttpClient())
            {
                c.BaseAddress = new Uri(client.ApiHost);
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Add("Authorization", client.ApiKey);

                var result = await requestMethodFinder
                    .FindFor(c, request)
                    .Execute(request);

                return new Response
                {
                    StatusCode = result.StatusCode,
                    ReasonPhrase = result.ReasonPhrase,
                    Content = await result.Content.ReadAsStringAsync()
                };
            }
        }
    }
}