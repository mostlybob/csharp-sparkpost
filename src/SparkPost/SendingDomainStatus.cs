using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SparkPost
{
    public class SendingDomainStatus
    {
        [JsonProperty("ownership_verified")]
        public bool OwnershipVerified { get; set; }

        [JsonProperty("dkim_status")]
        public DkimStatus DkimStatus { get; set; }

        [JsonProperty("spf_status")]
        public SpfStatus SpfStatus { get; set; }

        [JsonProperty("abuse_at_status")]
        public AbuseAtStatus AbuseAtStatus { get; set; }

        [JsonProperty("postmaster_at_status")]
        public PostmasterAtStatus PostmasterAtStatus { get; set; }

        [JsonProperty("compliance_status")]
        public ComplianceStatus ComplianceStatus { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DkimStatus
    {
        [EnumMember(Value = "unverified")]
        Unverified,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "invalid")]
        Invalid,
        [EnumMember(Value = "valid")]
        Valid
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpfStatus
    {
        [EnumMember(Value = "unverified")]
        Unverified,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "invalid")]
        Invalid,
        [EnumMember(Value = "valid")]
        Valid
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AbuseAtStatus
    {
        [EnumMember(Value = "unverified")]
        Unverified,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "invalid")]
        Invalid,
        [EnumMember(Value = "valid")]
        Valid
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PostmasterAtStatus
    {
        [EnumMember(Value = "unverified")]
        Unverified,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "invalid")]
        Invalid,
        [EnumMember(Value = "valid")]
        Valid
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ComplianceStatus
    {
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "valid")]
        Valid
    }
}