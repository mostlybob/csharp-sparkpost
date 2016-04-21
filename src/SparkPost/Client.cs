using System;
using System.Net.Http;

namespace SparkPost
{
    public class Client : IClient
    {
        public Client(string apiKey, string apiHost = "https://api.sparkpost.com")
        {
            ApiKey = apiKey;
            ApiHost = apiHost;
            Transmissions = new Transmissions(this, new RequestSender(this), new DataMapper(Version));
            Suppressions = new Suppressions(this, new RequestSender(this), new DataMapper());

            Settings = new CustomSettings();
        }

        public string ApiKey { get; set; }
        public string ApiHost { get; set; }

        public ITransmissions Transmissions { get; }
        public ISuppressions Suppressions { get; }
        public string Version => "v1";

        public CustomSettings Settings { get; }

        public class CustomSettings
        {
            private Func<HttpClient> httpClientBuilder;

            public CustomSettings()
            {
                httpClientBuilder = () => new HttpClient();
            }

            internal HttpClient CreateANewHttpClient()
            {
                return httpClientBuilder();
            }

            public void BuildHttpClientsUsing(Func<HttpClient> httpClient)
            {
                httpClientBuilder = httpClient;
            }
        }
    }
}