using System.Net.Http;
using System.Threading.Tasks;

namespace SparkPost.RequestMethods
{
    public class Delete : IRequestMethod
    {
        private readonly HttpClient client;

        public Delete(HttpClient client)
        {
            this.client = client;
        }

        public Task<HttpResponseMessage> Execute(Request request)
        {
            return client.DeleteAsync(request.Url);
        }
    }
}