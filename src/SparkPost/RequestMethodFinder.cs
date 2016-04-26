using System.Collections.Generic;
using System.Linq;
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
            return new List<IRequestMethod>
            {
                new Delete(client),
                new Post(client),
                new Put(client),
                new Get(client)
            }.First(x => x.CanExecute(request));
        }
    }
}