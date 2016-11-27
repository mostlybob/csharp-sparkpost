using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public class GetMetricsListResponse: Response
    {
        public IList<string> Results { get; set; }

        public GetMetricsListResponse()
        {
            Results = new List<string>();
        }        

        public GetMetricsListResponse(Response source)
        {
            this.SetFrom(source);
        }
    }
}
