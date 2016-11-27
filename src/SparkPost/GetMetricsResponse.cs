using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public class GetMetricsResponse: Response
    {
        public IList<IDictionary<MetricsField, object>> Results { get; set; }

        public GetMetricsResponse()
        {
            Results = new List<IDictionary<MetricsField, object>>();
        }

        public GetMetricsResponse(Response source)
        {
            this.SetFrom(source);
        }
    }
}
