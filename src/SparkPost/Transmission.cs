using System;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SparkPost
{
    public class Transmission
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
    }
}