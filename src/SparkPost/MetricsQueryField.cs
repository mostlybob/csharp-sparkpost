using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public sealed class MetricsQueryField
    {
        private readonly string _name;

        public static readonly MetricsQueryField Injected = new MetricsQueryField("count_injected");
        public static readonly MetricsQueryField Targeted = new MetricsQueryField("count_targeted");
        public static readonly MetricsQueryField Sent = new MetricsQueryField("count_sent");
        public static readonly MetricsQueryField Accepted = new MetricsQueryField("count_accepted");
        public static readonly MetricsQueryField Delivered = new MetricsQueryField("count_delivered");
        public static readonly MetricsQueryField DeliveredFirst = new MetricsQueryField("count_delivered_first");
        public static readonly MetricsQueryField DeliveredSubsequent = new MetricsQueryField("count_delivered_subsequent");
        public static readonly MetricsQueryField Rendered = new MetricsQueryField("count_rendered");
        public static readonly MetricsQueryField UniqueRendered = new MetricsQueryField("count_unique_rendered");
        public static readonly MetricsQueryField UniqueConfirmedOpened = new MetricsQueryField("count_unique_confirmed_opened");
        public static readonly MetricsQueryField Clicked = new MetricsQueryField("count_clicked");
        public static readonly MetricsQueryField UniqueClicked = new MetricsQueryField("count_unique_clicked");
        public static readonly MetricsQueryField Bounce = new MetricsQueryField("count_bounce");
        public static readonly MetricsQueryField HardBounce = new MetricsQueryField("count_hard_bounce");
        public static readonly MetricsQueryField SoftBounce = new MetricsQueryField("count_soft_bounce");
        public static readonly MetricsQueryField BlockBounce = new MetricsQueryField("count_block_bounce");
        public static readonly MetricsQueryField AdminBounce = new MetricsQueryField("count_admin_bounce");
        public static readonly MetricsQueryField UndeterminedBounce = new MetricsQueryField("count_undetermined_bounce");
        public static readonly MetricsQueryField Rejected = new MetricsQueryField("count_rejected");
        public static readonly MetricsQueryField PolicyRejection = new MetricsQueryField("count_policy_rejection");
        public static readonly MetricsQueryField GenerationFailed = new MetricsQueryField("count_generation_failed");
        public static readonly MetricsQueryField GenerationRejection = new MetricsQueryField("count_generation_rejection");
        public static readonly MetricsQueryField InBandBounce = new MetricsQueryField("count_inband_bounce");
        public static readonly MetricsQueryField OutOfBandBounce = new MetricsQueryField("count_outofband_bounce");
        public static readonly MetricsQueryField Delayed = new MetricsQueryField("count_delayed");
        public static readonly MetricsQueryField DelayedFirst = new MetricsQueryField("count_delayed_first");
        public static readonly MetricsQueryField MessageVolume = new MetricsQueryField("total_msg_volume");
        public static readonly MetricsQueryField SpamComplaint = new MetricsQueryField("count_spam_complaint");

        
        private MetricsQueryField(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }    
}
