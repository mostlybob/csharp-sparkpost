using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SparkPost.RequestSenders;
using System.Collections.Generic;
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

            // Serialize Message Events Query using specific DateTime format required by SparkPost Message Events API.
            var messageEventsQueryFixed = JsonConvert.DeserializeObject<IDictionary<string, string>>(JsonConvert.SerializeObject(messageEventsQuery, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm" }));

            var request = new Request
            {
                Url = $"/api/{client.Version}/message-events",
                Method = "GET",
                Data = messageEventsQueryFixed
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            ListMessageEventsResponse listMessageEventsResponse = JsonConvert.DeserializeObject<ListMessageEventsResponse>(response.Content);
            listMessageEventsResponse.ReasonPhrase = response.ReasonPhrase;
            listMessageEventsResponse.StatusCode = response.StatusCode;
            listMessageEventsResponse.Content = response.Content;

            return listMessageEventsResponse;
        }
    }
}