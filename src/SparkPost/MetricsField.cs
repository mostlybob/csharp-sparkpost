using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost
{
    public sealed class MetricsField
    {
        private readonly string _name;
        private static readonly Dictionary<string, MetricsField> _dict = new Dictionary<string, MetricsField>();

        public static readonly MetricsField Injected = new MetricsField("count_injected");
        public static readonly MetricsField Targeted = new MetricsField("count_targeted");
        public static readonly MetricsField Sent = new MetricsField("count_sent");
        public static readonly MetricsField Accepted = new MetricsField("count_accepted");
        public static readonly MetricsField Delivered = new MetricsField("count_delivered");
        public static readonly MetricsField DeliveredFirst = new MetricsField("count_delivered_first");
        public static readonly MetricsField DeliveredSubsequent = new MetricsField("count_delivered_subsequent");
        public static readonly MetricsField Rendered = new MetricsField("count_rendered");
        public static readonly MetricsField UniqueRendered = new MetricsField("count_unique_rendered");
        public static readonly MetricsField UniqueConfirmedOpened = new MetricsField("count_unique_confirmed_opened");
        public static readonly MetricsField Clicked = new MetricsField("count_clicked");
        public static readonly MetricsField UniqueClicked = new MetricsField("count_unique_clicked");
        public static readonly MetricsField Bounce = new MetricsField("count_bounce");
        public static readonly MetricsField HardBounce = new MetricsField("count_hard_bounce");
        public static readonly MetricsField SoftBounce = new MetricsField("count_soft_bounce");
        public static readonly MetricsField BlockBounce = new MetricsField("count_block_bounce");
        public static readonly MetricsField AdminBounce = new MetricsField("count_admin_bounce");
        public static readonly MetricsField UndeterminedBounce = new MetricsField("count_undetermined_bounce");
        public static readonly MetricsField Rejected = new MetricsField("count_rejected");
        public static readonly MetricsField PolicyRejection = new MetricsField("count_policy_rejection");
        public static readonly MetricsField GenerationFailed = new MetricsField("count_generation_failed");
        public static readonly MetricsField GenerationRejection = new MetricsField("count_generation_rejection");
        public static readonly MetricsField InBandBounce = new MetricsField("count_inband_bounce");
        public static readonly MetricsField OutOfBandBounce = new MetricsField("count_outofband_bounce");
        public static readonly MetricsField Delayed = new MetricsField("count_delayed");
        public static readonly MetricsField DelayedFirst = new MetricsField("count_delayed_first");
        public static readonly MetricsField MessageVolume = new MetricsField("total_msg_volume");
        public static readonly MetricsField SpamComplaint = new MetricsField("count_spam_complaint");

        public static readonly MetricsField Domain = new MetricsField("domain");
        public static readonly MetricsField SendingIp = new MetricsField("sending_ip");
        public static readonly MetricsField IpPool = new MetricsField("ip_pool");
        public static readonly MetricsField SendingDomain = new MetricsField("sending_domain");
        public static readonly MetricsField Subaccount = new MetricsField("subaccount_id");
        public static readonly MetricsField Campaign = new MetricsField("campaign_id");
        public static readonly MetricsField Template = new MetricsField("template_id");
        public static readonly MetricsField WatchedDomain = new MetricsField("watched_domain");
        public static readonly MetricsField TimeSeries = new MetricsField("ts");
        public static readonly MetricsField Reason = new MetricsField("reason");
        public static readonly MetricsField BounceClassName = new MetricsField("bounce_class_name");
        public static readonly MetricsField BounceClassDescription = new MetricsField("bounce_class_description");
        public static readonly MetricsField BounceCategoryId = new MetricsField("bounce_category_id");
        public static readonly MetricsField ClassificationId = new MetricsField("classification_id");
        public static readonly MetricsField BounceCategoryName = new MetricsField("bounce_category_name");
        public static readonly MetricsField RejectionCategoryId = new MetricsField("rejection_category_id");
        public static readonly MetricsField RejectionType = new MetricsField("rejection_type");
        public static readonly MetricsField LinkName = new MetricsField("link_name");
        public static readonly MetricsField RawClicked = new MetricsField("count_raw_clicked");
        public static readonly MetricsField Attempt = new MetricsField("attempt");


        private MetricsField(string name)
        {
            _name = name;
            _dict[name] = this;
        }

        public override string ToString()
        {
            return _name;
        }

        public static explicit operator MetricsField(string str)
        {
            MetricsField result;
            if (_dict.TryGetValue(str, out result))
                return result;
            else
                throw new InvalidCastException();
        }
    }    
}
