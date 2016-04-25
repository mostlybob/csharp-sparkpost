using System.Net.Http;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IRequestMethod
    {
        Task<HttpResponseMessage> Execute(Request request);
    }
}