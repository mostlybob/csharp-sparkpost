using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IMetrics
    {
        Task<GetMetricsResponse> GetDeliverability(object query);
        Task<GetMetricsResponse> GetDeliverabilityByDomain(object query);
        Task<GetMetricsResponse> GetDeliverabilityBySendingIp(object query);
        Task<GetMetricsResponse> GetDeliverabilityByIpPool(object query);
        Task<GetMetricsResponse> GetDeliverabilityBySendingDomain(object query);
        Task<GetMetricsResponse> GetDeliverabilityBySubAccount(object query);
        Task<GetMetricsResponse> GetDeliverabilityByCampaign(object query);
        Task<GetMetricsResponse> GetDeliverabilityByTemplate(object query);
        Task<GetMetricsResponse> GetDeliverabilityByWatchedDomain(object query);
        Task<GetMetricsResponse> GetDeliverabilityByTimeSeries(object query);
        Task<GetMetricsResponse> GetBounceReasons(object query);
        Task<GetMetricsResponse> GetBounceReasonsByDomain(object query);
        Task<GetMetricsResponse> GetBounceClassifications(object query);
        Task<GetMetricsResponse> GetRejectionReasons(object query);
        Task<GetMetricsResponse> GetRejectionReasonsByDomain(object query);
        Task<GetMetricsResponse> GetDelayReasons(object query);
        Task<GetMetricsResponse> GetDelayReasonsByDomain(object query);
        Task<GetMetricsResponse> GetEngagementByLink(object query);
        Task<GetMetricsResponse> GetDeliveriesByAttempt(object query);

        Task<GetMetricsResourceResponse> GetDomains();
        Task<GetMetricsResourceResponse> GetDomains(object metricsSimpleQuery);
        Task<GetMetricsResourceResponse> GetSendingIps();
        Task<GetMetricsResourceResponse> GetSendingIps(object metricsSimpleQuery);
        Task<GetMetricsResourceResponse> GetIpPools();
        Task<GetMetricsResourceResponse> GetIpPools(object metricsSimpleQuery);
        Task<GetMetricsResourceResponse> GetCampaigns();
        Task<GetMetricsResourceResponse> GetCampaigns(object metricsSimpleQuery);
    }
}
