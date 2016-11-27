using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IMetrics
    {
        Task<GetDeliverabilityResponse> GetDeliverability(object query);
        Task<GetMetricsListResponse> GetDomains();
        Task<GetMetricsListResponse> GetDomains(object metricsSimpleQuery);
        Task<GetMetricsListResponse> GetSendingIps();
        Task<GetMetricsListResponse> GetSendingIps(object metricsSimpleQuery);
        Task<GetMetricsListResponse> GetIpPools();
        Task<GetMetricsListResponse> GetIpPools(object metricsSimpleQuery);
        Task<GetMetricsListResponse> GetCampaigns();
        Task<GetMetricsListResponse> GetCampaigns(object metricsSimpleQuery);
    }
}
