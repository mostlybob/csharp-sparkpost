using System;
using System.Net.Http;
using SparkPost.RequestSenders;

namespace SparkPost
{
    public class Client : IClient
    {
        public Client(string apiKey, string apiHost = "https://api.sparkpost.com")
        {
            ApiKey = apiKey;
            ApiHost = apiHost;

            var dataMapper = new DataMapper(Version);
            var asyncRequestSender = new AsyncRequestSender(this);
            var syncRequestSender = new SyncRequestSender(asyncRequestSender);
            var requestSender = new RequestSender(asyncRequestSender, syncRequestSender, this);

            Transmissions = new Transmissions(this, requestSender, dataMapper);
            Suppressions = new Suppressions(this, requestSender, dataMapper);
            Webhooks = new Webhooks(this, requestSender, dataMapper);

            CustomSettings = new Settings();
        }

        public string ApiKey { get; set; }
        public string ApiHost { get; set; }

        public ITransmissions Transmissions { get; }
        public ISuppressions Suppressions { get; }
        public IWebhooks Webhooks { get; }
        public string Version => "v1";

        public Settings CustomSettings { get; }

        public class Settings
        {
            private Func<HttpClient> httpClientBuilder;

            public Settings()
            {
                httpClientBuilder = () => new HttpClient();
            }

            public SendingModeOptions SendingMode { get; set; }

            public enum SendingModeOptions
            {
                Async, Sync
            }

            public HttpClient CreateANewHttpClient()
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