using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost
{
    public class RequestSender
    {
        private readonly Client client;

        public RequestSender(Client client)
        {
            this.client = client;
        }

        public async Task<Response> Send(Request request)
        {
            using (var c = new HttpClient())
            {
                c.BaseAddress = new Uri(client.ApiHost);
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Add("Authorization", client.ApiKey);

                var result = await c.PostAsync(request.Url, BuildContent(request.Data));

                return new Response
                {
                    StatusCode = result.StatusCode,
                    ReasonPhrase = result.ReasonPhrase,
                    Content = await result.Content.ReadAsStringAsync(),
                };
            }
        }

        private static StringContent BuildContent(object data)
        {
            return new StringContent(JsonConvert.SerializeObject(data, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.None}));
        }
    }
}