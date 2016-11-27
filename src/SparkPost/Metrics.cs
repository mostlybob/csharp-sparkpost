using SparkPost.RequestSenders;
using SparkPost.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public class Metrics: IMetrics
    {
        private readonly IClient client;
        private readonly IRequestSender requestSender;

        public Metrics(IClient client, IRequestSender requestSender)
        {            
            this.client = client;
            this.requestSender = requestSender;
        }

        public async Task<IEnumerable<string>> ListDomains(object metricsSimpleQuery)
        {
            if (metricsSimpleQuery == null)
                metricsSimpleQuery = new { };

            var request = new Request
            {
                Url = $"/api/{client.Version}/metrics/domains",
                Method = "GET",
                Data = metricsSimpleQuery
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            dynamic content = Jsonification.DeserializeObject<dynamic>(response.Content);

            var result = ConvertToStrings(content.results.domains);
            return result;
        }

        private IEnumerable<string> ConvertToStrings(dynamic input)
        {
            var result = new List<string>();
            if (input == null) return result;

            foreach (var item in input)
                result.Add((string)item);

            return result;
        }
    }
}



// Don't need DELIMITER
// Fields returned depend on METRICS param sent in -- probably should be dictionary


/*
 * GET/api/v1/metrics/deliverability{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone}  NO LIMIT

GET/api/v1/metrics/deliverability/domain{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/sending-ip{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/ip-pool{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}	

GET/api/v1/metrics/deliverability/sending-domain{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/subaccount{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}	

GET/api/v1/metrics/deliverability/campaign{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/template{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/watched-domain{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/time-series{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,precision,metrics,timezone}  PRECISION

GET/api/v1/metrics/deliverability/bounce-reason{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/bounce-reason/domain{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/bounce-classification{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,metrics,timezone,limit}

GET/api/v1/metrics/deliverability/rejection-reason{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,timezone,limit}	NO METRICS

GET/api/v1/metrics/deliverability/rejection-reason/domain{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,timezone,limit}   NO METRICS

GET/api/v1/metrics/deliverability/delay-reason{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,timezone,limit}  NO METRICS

GET/api/v1/metrics/deliverability/delay-reason/domain{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,sending_domains,subaccounts,timezone,limit}  NO METRICS

GET/api/v1/metrics/deliverability/link-name{?from,to,delimiter,timezone,campaigns,templates,subaccounts,sending_domains,metrics,limit}   NO DOMAINS, SENDING_IPS, IP_POOLS, SUBACCOUNTS, TIMEZONE

GET/api/v1/metrics/deliverability/attempt{?from,to,delimiter,domains,campaigns,templates,sending_ips,ip_pools,bindings,binding_groups,sending_domains,subaccounts,timezone}  NO METRICS, LIMIT






GET/api/v1/metrics/ip-pools{?from,to,timezone,match,limit}

GET/api/v1/metrics/sending-ips{?from,to,timezone,match,limit}

GET/api/v1/metrics/campaigns{?from,to,timezone,limit,match}

GET/api/v1/metrics/domains{?from,to,timezone,limit,match}

    */