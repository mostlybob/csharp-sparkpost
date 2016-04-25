using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost.RequestMethods
{
    public class Put : IRequestMethod
    {
        private readonly HttpClient client;

        public Put(HttpClient client)
        {
            this.client = client;
        }

        public Task<HttpResponseMessage> Execute(Request request)
        {
            var content = new StringContent(SerializeObject(request.Data), Encoding.UTF8, "application/json");
            return client.PutAsync(request.Url, content);
        }

        private static string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.None});
        }
    }
}