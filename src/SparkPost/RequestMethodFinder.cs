using System.Net.Http;
using SparkPost.RequestMethods;

namespace SparkPost
{
    public interface IRequestMethodFinder
    {
        IRequestMethod FindFor(Request request);
    }

    public class RequestMethodFinder : IRequestMethodFinder
    {
        private readonly HttpClient client;

        public RequestMethodFinder(HttpClient client)
        {
            this.client = client;
        }

        public IRequestMethod FindFor(Request request)
        {
            switch (request.Method)
            {
                case "DELETE":
                    return new Delete(client);
                case "POST":
                    return new Post(client);
                case "PUT JSON":
                    return new Put(client);
                default:
                    return new Get(client);
            }
        }
    }
}