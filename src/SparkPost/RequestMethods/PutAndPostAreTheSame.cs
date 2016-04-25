using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost.RequestMethods
{
    public abstract class PutAndPostAreTheSame : IRequestMethod
    {
        protected readonly HttpClient Client;

        protected PutAndPostAreTheSame(HttpClient client)
        {
            Client = client;
        }

        public abstract bool CanExecute(Request request);

        public Task<HttpResponseMessage> Execute(Request request)
        {
            return Execute(request.Url, ContentFrom(request));
        }

        public abstract Task<HttpResponseMessage> Execute(string url, StringContent stringContent);

        private static StringContent ContentFrom(Request request)
        {
            return new StringContent(SerializeObject(request.Data), Encoding.UTF8, "application/json");
        }

        private static string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.None});
        }
    }
}