using System;
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

        public async Task<RetrieveTransmissionResponse> Retrieve(string transmissionId)
        {
            var request = new Request
            {
                Url = $"api/{client.Version}/transmissions/" + transmissionId,
                Method = "GET",
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;
            var transmissionResponse = new RetrieveTransmissionResponse()
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
            };

            if (results.transmission == null) return transmissionResponse;

            transmissionResponse.Id = results.transmission.id;
            transmissionResponse.Description = results.transmission.description;
            transmissionResponse.GenerationEndTime = results.transmission.generation_end_time;
            transmissionResponse.RecipientListTotalChunks = results.transmission.rcpt_list_total_chunks ?? 0;
            transmissionResponse.RecipientListTotalSize = results.transmission.rcpt_list_chunk_size ?? 0;
            transmissionResponse.NumberRecipients = results.transmission.num_rcpts ?? 0;
            transmissionResponse.NumberGenerated = results.transmission.num_generated ?? 0;

            return transmissionResponse;
        }

        public async Task<ListTransmissionResponse> List()
        {
            var request = new Request
            {
                Url = $"api/{client.Version}/transmissions",
                Method = "GET",
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            //var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;
            var transmissionResponse = new ListTransmissionResponse()
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
            };

            return transmissionResponse;
        }
    }
}