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

        public async Task<GetDeliverabilityResponse> GetDeliverability(object query)
        {
            var response = await GetMetricsResponse("deliverability", query);
            dynamic content = Jsonification.DeserializeObject<dynamic>(response.Content);

            var result = new GetDeliverabilityResponse(response);
            result.Results = ConvertToDictionary(content.results);

            return result;
        }

        private IList<IDictionary<MetricsField, object>> ConvertToDictionary(dynamic input)
        {
            var result = new List<IDictionary<MetricsField, object>>();
            if (input == null) return result;

            foreach (var array in input)
            {
                var dict = new Dictionary<MetricsField, object>();
                foreach (var item in array)
                {
                    var key = (MetricsField)item.Name;
                    dict.Add(key, item.Value);
                }
                result.Add(dict);
            }

            return result;
        }

        public async Task<GetMetricsListResponse> GetDomains()
        {
            return await GetDomains(null);
        }

        public async Task<GetMetricsListResponse> GetDomains(object metricsSimpleQuery)
        {
            return await GetSimpleList("domains", metricsSimpleQuery);
        }

        public async Task<GetMetricsListResponse> GetIpPools()
        {
            return await GetIpPools(null);
        }

        public async Task<GetMetricsListResponse> GetIpPools(object metricsSimpleQuery)
        {
            return await GetSimpleList("ip-pools", metricsSimpleQuery);
        }

        public async Task<GetMetricsListResponse> GetSendingIps()
        {
            return await GetSendingIps(null);
        }

        public async Task<GetMetricsListResponse> GetSendingIps(object metricsSimpleQuery)
        {
            return await GetSimpleList("sending-ips", metricsSimpleQuery);            
        }

        public async Task<GetMetricsListResponse> GetCampaigns()
        {
            return await GetCampaigns(null);
        }

        public async Task<GetMetricsListResponse> GetCampaigns(object metricsSimpleQuery)
        {
            return await GetSimpleList("campaigns", metricsSimpleQuery);
        }

        private async Task<GetMetricsListResponse> GetSimpleList(string listName, object query)
        {
            var response = await GetMetricsResponse(listName, query);
            dynamic content = Jsonification.DeserializeObject<dynamic>(response.Content);

            var result = new GetMetricsListResponse(response);
            result.Results = ConvertToStrings(content.results, listName);

            return result;            
        }

        private async Task<Response> GetMetricsResponse(string relUrl, object query)
        {
            if (query == null)
                query = new { };

            var request = new Request
            {
                Url = $"/api/{client.Version}/metrics/{relUrl}",
                Method = "GET",
                Data = query
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);
            return response;
        }

        private IList<string> ConvertToStrings(dynamic input, string propName)
        {
            var result = new List<string>();
            if (input == null) return result;

            foreach (var item in input[propName])
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