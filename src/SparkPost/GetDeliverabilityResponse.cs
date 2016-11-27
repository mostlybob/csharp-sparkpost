using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public class GetDeliverabilityResponse: Response
    {
        public IList<IDictionary<MetricsField, object>> Results { get; set; }

        public GetDeliverabilityResponse()
        {
            Results = new List<IDictionary<MetricsField, object>>();
        }

        public GetDeliverabilityResponse(Response source)
        {
            this.SetFrom(source);
        }
    }
}
