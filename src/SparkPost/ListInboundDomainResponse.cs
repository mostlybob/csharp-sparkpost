using System.Collections.Generic;
using Newtonsoft.Json;

namespace SparkPost
{
    public class ListInboundDomainResponse : Response
    {
        public IEnumerable<InboundDomain> InboundDomains { get; set; }

        public static ListInboundDomainResponse CreateFromResponse(Response response)
        {
            var result = new ListInboundDomainResponse();
            LeftRight.SetValuesToMatch(result, response);

            var results = JsonConvert.DeserializeObject<dynamic>(result.Content).results;
            var inboundDomains = new List<InboundDomain>();
            foreach(var r in results)
                inboundDomains.Add(ConvertToAInboundDomain(r));

            result.InboundDomains = inboundDomains;
            return result;
        }

        internal static InboundDomain ConvertToAInboundDomain(dynamic r)
        {
            var inboundDomain = new InboundDomain
            {
                Domain = r.domain
            };
            return inboundDomain;
        }
    }
}