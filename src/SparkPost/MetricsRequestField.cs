using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public class MetricsRequestField
    {
        public static string Injected = "count_injected";
        public static string Targeted = "count_targeted";
        public static string Sent = "count_sent";
        public static string Accepted = "count_accepted";
        public static string Delivered = "count_delivered";
        public static string DeliveredFirst = "count_delivered_first";
        public static string DeliveredSubsequent = "count_delivered_subsequent";
        public static string Rendered = "count_rendered";
        public static string UniqueRendered = "count_unique_rendered";
        public static string UniqueConfirmedOpened = "count_unique_confirmed_opened";
        public static string Clicked = "count_clicked";
        public static string UniqueClicked = "count_unique_clicked";
        public static string Bounce = "count_bounce";
        public static string HardBounce = "count_hard_bounce";
        public static string SoftBounce = "count_soft_bounce";
        public static string BlockBounce = "count_block_bounce";
        public static string AdminBounce = "count_admin_bounce";
        public static string UndeterminedBounce = "count_undetermined_bounce";
        public static string Rejected = "count_rejected";
        public static string PolicyRejection = "count_policy_rejection";
        public static string GenerationFailed = "count_generation_failed";
        public static string GenerationRejection = "count_generation_rejection";
        public static string InbandBounce = "count_inband_bounce";
        public static string OutOfBandBounce = "count_outofband_bounce";
        public static string Delayed = "count_delaed";
        public static string DelayedFirst = "count_delayed_first";
        public static string MessageVolume = "total_msg_volume";
        public static string SpamComplaint = "count_spam_complaint";
    }    
}
