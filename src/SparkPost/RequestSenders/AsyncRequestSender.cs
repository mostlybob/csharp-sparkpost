using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SparkPost.RequestSenders
{
    public class AsyncRequestSender : IRequestSender
    {
        private readonly Client client;

        public AsyncRequestSender(Client client)
        {
            this.client = client;
        }

        public async virtual Task<Response> Send(Request request)
        {
            using (var httpClient = client.CustomSettings.CreateANewHttpClient())
            {
                httpClient.BaseAddress = new Uri(client.ApiHost);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", client.ApiKey);
                if (client.SubaccountId != 0)
                {
                    httpClient.DefaultRequestHeaders.Add("X-MSYS-SUBACCOUNT", client.SubaccountId.ToString(CultureInfo.InvariantCulture));
                }

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