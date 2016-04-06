namespace SparkPost
{
    public class Client : IClient
    {
        public Client(string apiKey, string apiHost = "https://api.sparkpost.com")
        {
            ApiKey = apiKey;
            ApiHost = apiHost;
            Transmissions = new Transmissions(this, new RequestSender(this), new DataMapper(Version));
            Suppressions = new Suppressions(this, new RequestSender(this));
        }

        public string ApiKey { get; set; }
        public string ApiHost { get; set; }

        public ITransmissions Transmissions { get; }
        public Suppressions Suppressions { get; }
        public string Version => "v1";
    }
}