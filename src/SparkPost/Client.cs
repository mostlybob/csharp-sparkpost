namespace SparkPost
{
    public class Client
    {
        public Client(string apiKey, string apiHost = "https://api.sparkpost.com")
        {
            ApiKey = apiKey;
            ApiHost = apiHost;
            Transmissions = new Transmissions(this);
        }

        public string ApiKey { get; set; }
        public string ApiHost { get; set; }

        public Transmissions Transmissions { get; }
    }
}