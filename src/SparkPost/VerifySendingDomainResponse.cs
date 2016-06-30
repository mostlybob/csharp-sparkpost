using Newtonsoft.Json;
namespace SparkPost
{
    public class VerifySendingDomainResponse : Response
    {
        public VerifySendingDomainStatus Status { get; set; }
    }
}