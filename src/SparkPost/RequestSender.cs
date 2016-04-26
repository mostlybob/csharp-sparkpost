using System.Threading.Tasks;

namespace SparkPost
{
    public interface IRequestSender
    {
        Task<Response> Send(Request request);
    }

    public class RequestSender : IRequestSender
    {
        private readonly AsyncRequestSender asyncRequestSender;
        private readonly SyncRequestSender syncRequestSender;
        private readonly IClient client;

        public RequestSender(AsyncRequestSender asyncRequestSender, SyncRequestSender syncRequestSender, IClient client)
        {
            this.asyncRequestSender = asyncRequestSender;
            this.syncRequestSender = syncRequestSender;
            this.client = client;
        }

        public Task<Response> Send(Request request)
        {
            var requestSender = client.CustomSettings.SendingMode == "sync" ? syncRequestSender : asyncRequestSender as IRequestSender;
            return requestSender.Send(request);
        }
    }
}