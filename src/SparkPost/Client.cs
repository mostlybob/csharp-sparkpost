using System;
using System.Net.Http;

namespace SparkPost
{
    public class Client : IClient
    {
        private Func<HttpClient> httpClientBuilder;

        public Client(string apiKey, string apiHost = "https://api.sparkpost.com")
        {
            ApiKey = apiKey;
            ApiHost = apiHost;
            Transmissions = new Transmissions(this, new RequestSender(this), new DataMapper(Version));
            Suppressions = new Suppressions(this, new RequestSender(this), new DataMapper());

            httpClientBuilder = () => new HttpClient();
        }

        public string ApiKey { get; set; }
        public string ApiHost { get; set; }

        public ITransmissions Transmissions { get; }
        public ISuppressions Suppressions { get; }
        public string Version => "v1";

        internal HttpClient CreateANewHttpClient()
        {
            return httpClientBuilder();
        }

        public void BuildHttpClientsUsing(Func<HttpClient> httpClient)
        {
            this.httpClientBuilder = httpClient;
        }
    }
}