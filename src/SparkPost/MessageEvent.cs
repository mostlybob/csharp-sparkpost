using Newtonsoft.Json;
using SparkPost.Utilities;
using System;
using System.Collections.Generic;

namespace SparkPost
{
    public class MessageEvent
    {
        /// <summary>
        /// "type": {
        ///   "description": "Type of event this record describes",
        ///   "sampleValue": "bounce"
        /// }
        /// "type": "out_of_band",
        /// </summary>
        [JsonProperty("type")]
        public string TypeJson { get; set; }

        /// <summary>
        /// Type of event this record describes
        /// </summary>
        [JsonIgnore]
        public MessageEventType Type
        {
            get
            {
                foreach (string typeName in Enum.GetNames(typeof(MessageEventType)))
                {
                    string typeNameSnakeCase = SnakeCase.Convert(typeName);
                    if (string.Equals(this.TypeJson, typeNameSnakeCase, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return (MessageEventType)Enum.Parse(typeof(MessageEventType), typeName);
                    }
                }
                return MessageEventType.Undefined;
            }
        }

        /// <summary>
        /// "bounce_class": {
        ///   "description": "Classification code for a given message (see [Bounce Classification Codes](https://support.sparkpost.com/customer/portal/articles/1929896))",
        ///   "sampleValue": "1"
        /// },
        /// "bounce_class": "10",
        /// </summary>
        [JsonProperty("bounce_class")]
        public string BounceClassJson { get; set; }

        /// <summary>
        /// Classification code for a given message (see [Bounce Classification Codes](https://support.sparkpost.com/customer/portal/articles/1929896))
        /// </summary>
        [JsonIgnore]
        public BounceClass BounceClass
        {
            get
            {
                int bounceClassAsInt;
                if (int.TryParse(this.BounceClassJson, out bounceClassAsInt))
                {
                    BounceClass bounceClass = (BounceClass)bounceClassAsInt;
                    if (bounceClass.ToString() == bounceClassAsInt.ToString()) // Enum value not found - ToString() returns a number
                    {
                        return BounceClass.Undefined;
                    }
                    return bounceClass;
                }
                return BounceClass.Undefined;
            }
        }

        /// <summary>
        /// Classification code for a given message (see [Bounce Classification Codes](https://support.sparkpost.com/customer/portal/articles/1929896))
        /// </summary>
        [JsonIgnore]
        public BounceClassDetails BounceClassDetails
        {
            get
            {
                return BounceClassesDetails.AllBounceClasses[this.BounceClass];
            }
        }

        /// <summary>
        /// "campaign_id": {
        ///   "description": "Campaign of which this message was a part",
        ///   "sampleValue": "Example Campaign Name"
        /// },
        /// "campaign_id": "My campaign name",
        /// </summary>
        [JsonProperty("campaign_id")]
        public string CampaignId { get; set; }

        /// <summary>
        /// "customer_id": {
        ///   "description": "SparkPost-customer identifier through which this message was sent",
        ///   "sampleValue": "1"
        /// },
        /// "customer_id": "12345",
        /// </summary>
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        /// <summary>
        /// "delv_method": {
        ///   "description": "Protocol by which SparkPost delivered this message",
        ///   "sampleValue": "esmtp"
        /// },
        /// "delv_method": "esmtp",
        /// </summary>
        [JsonProperty("delv_method")]
        public string DeliveryMethod { get; set; }

        /// <summary>
        /// "device_token": {
        ///   "description": "Token of the device / application targeted by this PUSH notification message. Applies only when delv_method is gcm or apn.",
        ///   "sampleValue": "45c19189783f867973f6e6a5cca60061ffe4fa77c547150563a1192fa9847f8a"
        /// },
        /// </summary>
        [JsonProperty("device_token")]
        public string DeviceToken { get; set; }

        /// <summary>
        /// "error_code": {
        ///   "description": "Error code by which the remote server described a failed delivery attempt",
        ///   "sampleValue": "554"
        /// },
        /// "error_code": "550",
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// "ip_address": {
        ///   "description": "IP address of the host to which SparkPost delivered this message; in engagement events, the IP address of the host where the HTTP request originated",
        ///   "sampleValue": "127.0.0.1"
        /// },
        /// </summary>
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        /// <summary>
        /// "message_id": {
        ///   "description": "SparkPost-cluster-wide unique identifier for this message",
        ///   "sampleValue": "0e0d94b7-9085-4e3c-ab30-e3f2cd9c273e"
        /// },
        /// "message_id": "00021f9a27476a273c57",
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        /// <summary>
        /// "msg_from": {
        ///   "description": "Sender address used on this message's SMTP envelope",
        ///   "sampleValue": "sender@example.com"
        /// },
        /// "msg_from": "msprvs1=17827RA6TC8Pz=bounces-12345@sparkpostmail1.com",
        /// </summary>
        [JsonProperty("msg_from")]
        public string MessageForm { get; set; }

        /// <summary>
        /// "msg_size": {
        ///   "description": "Message's size in bytes",
        ///   "sampleValue": "1337"
        /// },
        /// "msg_size": "3168",
        /// </summary>
        [JsonProperty("msg_size")]
        public string MessageSize { get; set; }

        /// <summary>
        /// "num_retries": {
        ///   "description": "Number of failed attempts before this message was successfully delivered; when the first attempt succeeds, zero",
        ///   "sampleValue": "2"
        /// },
        /// "num_retries": "0",
        /// </summary>
        [JsonProperty("num_retries")]
        public string NumberOfRetries { get; set; }

        /// <summary>
        /// "rcpt_meta": {
        ///   "description": "Metadata describing the message recipient",
        ///   "sampleValue": {
        ///     "customKey": "customValue"
        ///   }
        /// },
        ///"rcpt_meta": {
        ///  "CustomKey1": "Custom Value 1",
        ///  "CustomKey2": "Custom Value 2"
        ///},
        /// </summary>
        [JsonProperty("rcpt_meta")]
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// "rcpt_tags": {
        ///   "description": "Tags applied to the message which generated this event",
        ///   "sampleValue": [
        ///     "male",
        ///     "US"
        ///   ]
        /// },
        /// "rcpt_tags": [ "CustomTag1" ],
        /// </summary>
        [JsonProperty("rcpt_tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// "rcpt_to": {
        ///   "description": "Recipient address used on this message's SMTP envelope",
        ///   "sampleValue": "recipient@example.com"
        /// },
        /// "rcpt_to": "to@domain.com",
        /// </summary>
        [JsonProperty("rcpt_to")]
        public string RecipientTo { get; set; }

        /// <summary>
        /// "rcpt_type": {
        ///   "description": "Indicates that a recipient address appeared in the Cc or Bcc header or the archive JSON array",
        ///   "sampleValue": "cc"
        /// },
        /// </summary>
        [JsonProperty("rcpt_type")]
        public string RecipientType { get; set; }

        /// <summary>
        /// "raw_reason": {
        ///   "description": "Unmodified, exact response returned by the remote server due to a failed delivery attempt",
        ///   "sampleValue": "MAIL REFUSED - IP (17.99.99.99) is in black list"
        /// },
        /// "raw_reason": "550 [internal] [oob] The recipient is invalid.",
        /// </summary>
        [JsonProperty("raw_reason")]
        public string RawReason { get; set; }

        /// <summary>
        /// "reason": {
        ///   "description": "Canonicalized text of the response returned by the remote server due to a failed delivery attempt",
        ///   "sampleValue": "MAIL REFUSED - IP (a.b.c.d) is in black list"
        /// },
        /// "reason": "550 [internal] [oob] The recipient is invalid.",
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// "routing_domain": {
        ///   "description": "Domain receiving this message",
        ///   "sampleValue": "example.com"
        /// },
        /// "routing_domain": "domain.com",
        /// </summary>
        [JsonProperty("routing_domain")]
        public string RoutingDomain { get; set; }

        /// <summary>
        /// "subject": {
        ///   "description": "Subject line from the email header",
        ///   "sampleValue": "Summer deals are here!"
        /// },
        /// "subject": "My email subject",
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// "template_id": {
        ///   "description": "Slug of the template used to construct this message",
        ///   "sampleValue": "templ-1234"
        /// },
        /// "template_id": "smtp_47287131967131576",
        /// </summary>
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// "template_version": {
        ///   "description": "Version of the template used to construct this message",
        ///   "sampleValue": "1"
        /// },
        /// "template_version": "0",
        /// </summary>
        [JsonProperty("template_version")]
        public string TemplateVersion { get; set; }

        /// <summary>
        /// "timestamp": {
        ///   "description": "Event date and time, in Unix timestamp format (integer seconds since 00:00:00 GMT 1970-01-01)",
        ///   "sampleValue": 1427736822
        /// },
        /// "timestamp": "2016-04-27T10:54:25.000+00:00"
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// "transmission_id": {
        ///   "description": "Transmission which originated this message",
        ///   "sampleValue": "64836157861974168"
        /// }
        /// "transmission_id": "47287131967131576",
        /// </summary>
        [JsonProperty("transmission_id")]
        public string TransmissionId { get; set; }

        /// <summary>
        /// Not documented.
        /// "event_id": "84320004715329757",
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        /// <summary>
        /// Not documented.
        /// "friendly_from": "from@domain.com",
        /// </summary>
        [JsonProperty("friendly_from")]
        public string FriendlyFrom { get; set; }

        /// <summary>
        /// Not documented.
        /// "ip_pool": "shared",
        /// </summary>
        [JsonProperty("ip_pool")]
        public string IpPool { get; set; }

        /// <summary>
        /// Not documented.
        /// "queue_time": "3004",
        /// </summary>
        [JsonProperty("queue_time")]
        public string QueueTime { get; set; }

        /// <summary>
        /// Not documented.
        /// "raw_rcpt_to": "to@domain.com",
        /// </summary>
        [JsonProperty("raw_rcpt_to")]
        public string RawRecipientTo { get; set; }

        /// <summary>
        /// Not documented.
        /// "sending_ip": "shared",
        /// </summary>
        [JsonProperty("sending_ip")]
        public string SendingIp { get; set; }

        /// <summary>
        /// Not documented.
        /// "tdate": "2016-04-27T22:05:40.000Z",
        /// </summary>
        [JsonProperty("tdate")]
        public DateTime TDate { get; set; }

        /// <summary>
        /// Not documented.
        /// "transactional": "1",
        /// </summary>
        [JsonProperty("transactional")]
        public string Transactional { get; set; }

        /// <summary>
        /// Not documented.
        /// "remote_addr": "A.B.C.D:25512",
        /// </summary>
        [JsonProperty("remote_addr")]
        public string RemoteAddress { get; set; }

        public override string ToString()
        {
            return string.Format("{0} from {1} to {2}", this.Type, this.FriendlyFrom, this.RecipientTo);
        }
    }
}