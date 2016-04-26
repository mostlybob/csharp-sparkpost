using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace SparkPost.RequestMethods
{
    public class Get : IRequestMethod
    {
        private readonly HttpClient client;

        public Get(HttpClient client)
        {
            this.client = client;
        }

        public bool CanExecute(Request request)
        {
            return (request.Method ?? "").ToLower().StartsWith("get");
        }

        public Task<HttpResponseMessage> Execute(Request request)
        {
            return client.GetAsync(string.Join("?",
                new[] {request.Url, ConvertToQueryString(request.Data)}
                    .Where(x => string.IsNullOrEmpty(x) == false)));
        }

        private static string ConvertToQueryString(object data)
        {
            if (data == null) return null;
            var dictionary =
                JsonConvert.DeserializeObject<IDictionary<string, string>>(JsonConvert.SerializeObject(data));

            var values = dictionary
                .Where(x => string.IsNullOrEmpty(x.Value) == false)
                .Select(x => HttpUtility.UrlEncode(DataMapper.ToSnakeCase(x.Key)) + "=" + HttpUtility.UrlEncode(x.Value));

            return string.Join("&", values);
        }
    }
}