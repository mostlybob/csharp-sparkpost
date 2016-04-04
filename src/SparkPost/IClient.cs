namespace SparkPost
{
    public interface IClient
    {
        string ApiKey { get; set; }
        string ApiHost { get; set; }
        Transmissions Transmissions { get; }
        string Version { get; }
    }
}