using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IMetrics
    {
        Task<IEnumerable<string>> GetDomains();
        Task<IEnumerable<string>> GetDomains(object metricsSimpleQuery);
        Task<IEnumerable<string>> GetSendingIps();
        Task<IEnumerable<string>> GetSendingIps(object metricsSimpleQuery);
        Task<IEnumerable<string>> GetIpPools();
        Task<IEnumerable<string>> GetIpPools(object metricsSimpleQuery);
        Task<IEnumerable<string>> GetCampaigns();
        Task<IEnumerable<string>> GetCampaigns(object metricsSimpleQuery);
    }
}
