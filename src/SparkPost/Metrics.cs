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

        #region Metrics
        public async Task<GetMetricsResponse> GetDeliverability(object query)
        {
            return await GetMetrics("deliverability", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityByDomain(object query)
        {
            return await GetMetrics("deliverability/domain", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityBySendingIp(object query)
        {
            return await GetMetrics("deliverability/sending-ip", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityByIpPool(object query)
        {
            return await GetMetrics("deliverability/ip-pool", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityBySendingDomain(object query)
        {
            return await GetMetrics("deliverability/sending-domain", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityBySubaccount(object query)
        {
            return await GetMetrics("deliverability/subaccount", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityByCampaign(object query)
        {
            return await GetMetrics("deliverability/campaign", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityByTemplate(object query)
        {
            return await GetMetrics("deliverability", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityByWatchedDomain(object query)
        {
            return await GetMetrics("deliverability/watched-domain", query);
        }

        public async Task<GetMetricsResponse> GetDeliverabilityByTimeSeries(object query)
        {
            return await GetMetrics("deliverability/time-series", query);
        }

        public async Task<GetMetricsResponse> GetBounceReasons(object query)
        {
            return await GetMetrics("deliverability/bounce-reason", query);
        }

        public async Task<GetMetricsResponse> GetBounceReasonsByDomain(object query)
        {
            return await GetMetrics("deliverability/bounce-reason/domain", query);
        }

        public async Task<GetMetricsResponse> GetBounceClassifications(object query)
        {
            return await GetMetrics("deliverability/bounce-classification", query);
        }

        public async Task<GetMetricsResponse> GetRejectionReasons(object query)
        {
            return await GetMetrics("deliverability/rejection-reason", query);
        }

        public async Task<GetMetricsResponse> GetRejectionReasonsByDomain(object query)
        {
            return await GetMetrics("deliverability/rejection-reason/domain", query);
        }

        public async Task<GetMetricsResponse> GetDelayReasons(object query)
        {
            return await GetMetrics("deliverability/delay-reason", query);
        }

        public async Task<GetMetricsResponse> GetDelayReasonsByDomain(object query)
        {
            return await GetMetrics("deliverability/delay-reason/domain", query);
        }

        public async Task<GetMetricsResponse> GetEngagementDetails(object query)
        {
            return await GetMetrics("deliverability/link-name", query);
        }

        public async Task<GetMetricsResponse> GetDeliveriesByAttempt(object query)
        {
            return await GetMetrics("deliverability/attempt", query);
        }
        #endregion

        #region Resources
        public async Task<GetMetricsResourceResponse> GetDomains()
        {
            return await GetDomains(null);
        }

        public async Task<GetMetricsResourceResponse> GetDomains(object metricsSimpleQuery)
        {
            return await GetMetricsResource("domains", metricsSimpleQuery);
        }

        public async Task<GetMetricsResourceResponse> GetIpPools()
        {
            return await GetIpPools(null);
        }

        public async Task<GetMetricsResourceResponse> GetIpPools(object metricsSimpleQuery)
        {
            return await GetMetricsResource("ip-pools", metricsSimpleQuery);
        }

        public async Task<GetMetricsResourceResponse> GetSendingIps()
        {
            return await GetSendingIps(null);
        }

        public async Task<GetMetricsResourceResponse> GetSendingIps(object metricsSimpleQuery)
        {
            return await GetMetricsResource("sending-ips", metricsSimpleQuery);            
        }

        public async Task<GetMetricsResourceResponse> GetCampaigns()
        {
            return await GetCampaigns(null);
        }

        public async Task<GetMetricsResourceResponse> GetCampaigns(object metricsSimpleQuery)
        {
            return await GetMetricsResource("campaigns", metricsSimpleQuery);
        }
        #endregion

        private async Task<GetMetricsResourceResponse> GetMetricsResource(string resourceName, object query)
        {
            var response = await GetApiResponse(resourceName, query);
            dynamic content = Jsonification.DeserializeObject<dynamic>(response.Content);

            var result = new GetMetricsResourceResponse(response);
            result.Results = ConvertToStrings(content.results, resourceName);

            return result;            
        }

        private async Task<GetMetricsResponse> GetMetrics(string relUrl, object query)
        {
            var response = await GetApiResponse(relUrl, query);
            dynamic content = Jsonification.DeserializeObject<dynamic>(response.Content);

            var result = new GetMetricsResponse(response);
            result.Results = ConvertToDictionaries(content.results);

            return result;
        }

        private async Task<Response> GetApiResponse(string relUrl, object query)
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

        private IList<IDictionary<string, object>> ConvertToDictionaries(dynamic input)
        {
            var result = new List<IDictionary<string, object>>();
            if (input == null) return result;

            foreach (var array in input)
            {
                var dict = new Dictionary<string, object>();
                foreach (var item in array)
                {
                    var key = (string)item.Name;
                    var val = item.Value.ToObject<object>();
                    dict.Add(key, val);
                }
                result.Add(dict);
            }

            return result;
        }
    }
}