using System;
using System.Globalization;
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

        public RequestSender(Client client)
        {
            this.client = client;
        }

        public async Task<Response> Send(Request request)
        {
            using (var httpClient = client.CustomSettings.CreateANewHttpClient())
            {
                httpClient.BaseAddress = new Uri(client.ApiHost);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", client.ApiKey);
                httpClient.DefaultRequestHeaders.Add("X-MSYS-SUBACCOUNT", client.SubaccountId.ToString(CultureInfo.InvariantCulture));

                var result = await new RequestMethodFinder(httpClient)
                    .FindFor(request)
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