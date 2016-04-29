using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SparkPost.Utilities;

namespace SparkPost.RequestMethods
{
    public class Get : RequestMethod
    {
        private readonly HttpClient client;

        public Get(HttpClient client)
        {
            this.client = client;
        }

        public override Task<HttpResponseMessage> Execute(Request request)
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
                .Select(x => HttpUtility.UrlEncode(SnakeCase.Convert(x.Key)) + "=" + HttpUtility.UrlEncode(x.Value));

            return string.Join("&", values);
        }
    }
}