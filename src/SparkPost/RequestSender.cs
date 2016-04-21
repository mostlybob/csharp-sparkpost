using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

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
            using (var c = client.Settings.CreateANewHttpClient())
            {
                c.BaseAddress = new Uri(client.ApiHost);
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Add("Authorization", client.ApiKey);

                HttpResponseMessage result;
                switch (request.Method)
                {
                    case "DELETE":
                        result = await c.DeleteAsync(request.Url);
                        break;
                    case "POST":
                        result = await c.PostAsync(request.Url, BuildContent(request.Data));
                        break;
                    case "PUT JSON":
                        var content = new StringContent(SerializeObject(request.Data), Encoding.UTF8, "application/json");
                        result = await c.PutAsync(request.Url, content);
                        break;
                    default:
                        result = await c.GetAsync(string.Join("?",
                            new[] {request.Url, ConvertToQueryString(request.Data)}
                                .Where(x => string.IsNullOrEmpty(x) == false)));
                        break;
                }

                return new Response
                {
                    StatusCode = result.StatusCode,
                    ReasonPhrase = result.ReasonPhrase,
                    Content = await result.Content.ReadAsStringAsync(),
                };
            }
        }

        private static string ConvertToQueryString(object data)
        {
            if (data == null) return null;
            var dictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(JsonConvert.SerializeObject(data));

            var values = dictionary
                .Where(x => string.IsNullOrEmpty(x.Value) == false)
                .Select(x => HttpUtility.UrlEncode(DataMapper.ToSnakeCase(x.Key)) + "=" + HttpUtility.UrlEncode(x.Value));

            return string.Join("&", values);
        }

        private static StringContent BuildContent(object data)
        {
            return new StringContent(SerializeObject(data));
        }

        private static string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.None});
        }
    }
}