using System.Net;
using System.Threading.Tasks;

namespace SparkPost
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public string Content { get; set; }
    }
}