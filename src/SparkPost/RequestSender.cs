using System.Threading.Tasks;

namespace SparkPost
{
    public interface IRequestSender
    {
        Task<Response> Send(Request request);
    }

    public class RequestSender : AsyncRequestSender
    {
        public RequestSender(Client client) : base(client)
        {
        }
    }
}