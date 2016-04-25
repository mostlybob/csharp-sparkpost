using System.Net.Http;
using SparkPost.RequestMethods;

namespace SparkPost
{
    public interface IRequestMethodFinder
    {
        IRequestMethod FindFor(HttpClient client, Request request);
    }

    public class RequestMethodFinder : IRequestMethodFinder
    {
        public IRequestMethod FindFor(HttpClient client, Request request)
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