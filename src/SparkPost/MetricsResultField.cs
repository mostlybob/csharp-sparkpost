using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public class MetricsResultField: MetricsRequestField
    {
        public static string Domain = "domain";
        public static string SendingIp = "sending_ip";
        public static string IpPool = "ip_pool";
        public static string SendingDomain = "sending_domain";
        public static string Subaccount = "subaccount_id";
        public static string Campaign = "campaign_id";
        public static string Template = "template_id";
        public static string WatchedDomain = "watched_domain";
        public static string TimeSeries = "ts";
        public static string Reason = "reason";
        public static string BounceClassName = "bounce_class_name";
        public static string BounceClassDescription = "bounce_class_description";
        public static string BounceCategoryId = "bounce_category_id";
        public static string ClassificationId = "classification_id";
        public static string BounceCategoryName = "bounce_category_name";
        public static string RejectionCategoryId = "rejection_category_id";
        public static string RejectionType = "rejection_type";
        public static string LinkName = "link_name";
        public static string RawClicked = "count_raw_clicked";
        public static string Attempt = "attempt";        
    }
}
