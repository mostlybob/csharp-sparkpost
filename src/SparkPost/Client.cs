using System;
using System.Net.Http;

namespace SparkPost
{
    public class Client : IClient
    {
        private const string defaultApiHost = "https://api.sparkpost.com";

        public Client(string apiKey) : this(apiKey, defaultApiHost, 0)
        {

        }

        public Client(string apiKey, string apiHost) : this(apiKey, apiHost, 0)
        {

        }

        public Client(string apiKey, long subAccountId) : this(apiKey, defaultApiHost, subAccountId)
        {

        }

        public Client(string apiKey, string apiHost, long subAccountId)
        {
            ApiKey = apiKey;
            ApiHost = apiHost;
            Transmissions = new Transmissions(this, new RequestSender(this), new DataMapper(Version));
            Suppressions = new Suppressions(this, new RequestSender(this), new DataMapper());
            Webhooks = new Webhooks(this, new RequestSender(this), new DataMapper());
            CustomSettings = new Settings();
        }

        public string ApiKey { get; set; }
        public string ApiHost { get; set; }
        public long SubaccountId { get; set; }

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