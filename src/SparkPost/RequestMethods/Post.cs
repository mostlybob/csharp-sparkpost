using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost.RequestMethods
{
    public class Post : IRequestMethod
    {
        private readonly HttpClient client;

        public Post(HttpClient client)
        {
            this.client = client;
        }

        public bool CanExecute(Request request)
        {
            throw new System.NotImplementedException();
        }

        public Task<HttpResponseMessage> Execute(Request request)
        {
            var postContent = new StringContent(SerializeObject(request.Data), Encoding.UTF8, "application/json");
            return client.PostAsync(request.Url, postContent);
        }

        private static string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.None});
        }
    }
}