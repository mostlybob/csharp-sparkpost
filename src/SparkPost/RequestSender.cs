using System;
using System.Net.Http;
using System.Threading.Tasks;
using SparkPost.RequestMethods;

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
            using (var c = client.CustomSettings.CreateANewHttpClient())
            {
                c.BaseAddress = new Uri(client.ApiHost);
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Add("Authorization", client.ApiKey);

                HttpResponseMessage result;
                switch (request.Method)
                {
                    case "DELETE":
                        result = await new Delete(c).Execute(request);
                        break;
                    case "POST":
                        result = await new Post(c).Execute(request);
                        break;
                    case "PUT JSON":
                        result = await new Put(c).Execute(request);
                        break;
                    default:
                        result = await new Get(c).Execute(request);
                        break;
                }

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