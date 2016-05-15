using Newtonsoft.Json;

namespace SparkPost
{
    public class InboundDomainResponse : Response
    {
        public InboundDomain InboundDomain { get; set; }

        public static InboundDomainResponse CreateFromResponse(Response response)
        {
            var result = new InboundDomainResponse();
            LeftRight.SetValuesToMatch(result, response);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;

            result.InboundDomain = ListInboundDomainResponse.ConvertToAInboundDomain(results);

            return result;
        }
    }
}