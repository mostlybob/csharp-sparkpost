using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost
{
    public class Transmissions
    {
        private readonly Client client;

        public Transmissions(Client client)
        {
            this.client = client;
        }

        public async Task<Response> Send(Transmission transmission)
        {
            var request = new Request
            {
                Method = "api/v1/transmissions",
                Data = transmission.ToDictionary()
            };

            return await client.Process(request);
        }
    }

    public class TransmissionDemo
    {
        public async Task<HttpResponseMessage> FireAnEmail(string apiKey)
        {
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri("https://api.sparkpost.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", apiKey);

                dynamic content = new ExpandoObject();
                content.from = "testing@sparkpostbox.com";
                content.subject = "Oh hey";
                content.text = "Testing";

                dynamic recipient = new ExpandoObject();
                recipient.address = "darren@cauthon.com";

                dynamic data = new ExpandoObject();
                data.content = content;
                data.recipients = new[] {recipient};

                var theString = JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(JsonConvert.SerializeObject(data));

                return await client.PostAsync("api/v1/transmissions", stringContent);
            }
        }

        public async Task<HttpResponseMessage> FireATemplate(string apiKey)
        {
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri("https://api.sparkpost.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", apiKey);

                var content = new Dictionary<string, object>();
                content["from"] = "testing@sparkpostbox.com";
                content["template_id"] = "my-first-email";
                //content.subject = "Oh hey";
                //content.text = "Testing";

                var substitutionData = new Dictionary<string, object>();
                substitutionData["first_name"] = "darren";
                substitutionData["second_name"] = "cauthon";

                dynamic recipient = new ExpandoObject();
                recipient.address = "darren@cauthon.com";

                var data = new Dictionary<string, Object>();
                data["content"] = content;
                data["recipients"] = new dynamic[] { recipient };
                data["substitution_data"] = substitutionData;

                var stringContent = new StringContent(JsonConvert.SerializeObject(data));

                return await client.PostAsync("api/v1/transmissions", stringContent);
            }
        }
    }
}