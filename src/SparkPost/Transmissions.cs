using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost
{
    public class Transmissions
    {
        private readonly Client client;
        private readonly RequestSender requestSender;
        private readonly DataMapper dataMapper;

        public Transmissions(Client client, RequestSender requestSender, DataMapper dataMapper)
        {
            this.client = client;
            this.requestSender = requestSender;
            this.dataMapper = dataMapper;
        }

        public async Task<SendTransmissionResponse> Send(Transmission transmission)
        {
            var request = new Request
            {
                Url = $"api/{client.Version}/transmissions",
                Method = "POST",
                Data = dataMapper.ToDictionary(transmission)
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;
            return new SendTransmissionResponse()
            {
                Id = results.id,
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                TotalAcceptedRecipients = results.total_accepted_recipients,
                TotalRejectedRecipients = results.total_rejected_recipients,
            };
        }
    }
}