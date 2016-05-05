using Newtonsoft.Json;
using SparkPost.RequestSenders;
using System.Net;
using System.Threading.Tasks;

namespace SparkPost
{
    public class MessageEvents : IMessageEvents
    {
        private readonly IClient client;
        private readonly IRequestSender requestSender;
        private readonly IDataMapper dataMapper;

        public MessageEvents(IClient client, IRequestSender requestSender, IDataMapper dataMapper)
        {
            this.client = client;
            this.requestSender = requestSender;
            this.dataMapper = dataMapper;
        }

        public async Task<ListMessageEventsResponse> List(MessageEventsQuery messageEventsQuery)
        {
            return await List(messageEventsQuery as object);
        }

        public async Task<ListMessageEventsResponse> List(object messageEventsQuery = null)
        {
            if (messageEventsQuery == null) messageEventsQuery = new { };

            var request = new Request
            {
                Url = $"/api/{client.Version}/message-events",
                Method = "GET",
                Data = messageEventsQuery
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var listMessageEventsResponse = JsonConvert.DeserializeObject<ListMessageEventsResponse>(response.Content);
            listMessageEventsResponse.ReasonPhrase = response.ReasonPhrase;
            listMessageEventsResponse.StatusCode = response.StatusCode;
            listMessageEventsResponse.Content = response.Content;

            return listMessageEventsResponse;
        }
    }
}