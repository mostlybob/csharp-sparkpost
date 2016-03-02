using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SparkPost.Tests
{

    [TestFixture]
    public class TransmissionsTests
    {
        private Transmissions transmissions;
        private string apiKey;
        private string apiHost;

        [SetUp]
        public void Setup()
        {
            apiKey = Guid.NewGuid().ToString();
            apiHost = Guid.NewGuid().ToString();
            transmissions = new Transmissions(apiKey, apiHost);
        }

        [Test]
        public void It_should_be_able_to_send_an_email()
        {
            var transmission = new Transmission
            {
                Content =
                {
                    From = new Address {Email = "testing@sparkpostbox.com"},
                    Subject = "Oh hey",
                    Text = "Testing"
                }
            };

            transmission.Recipients.Add(new Recipient {Address = new Address {Email = "darren@cauthon.com"}});

            var send = transmissions.Send(transmission);
            send.Wait();
        }
    }

    //[TestFixture]
    //public class TransmissionSenderTests
    //{
    //    [Test]
    //    public void FireAnEmail()
    //    {
    //        var transmission = new SparkPost.TransmissionDemo();
    //        var result = transmission.FireAnEmail("");
    //        result.Wait();
    //    }

    //    [Test]
    //    public void FireATemplate()
    //    {
    //        var transmission = new SparkPost.TransmissionDemo();
    //        var result = transmission.FireATemplate("");
    //        result.Wait();
    //    }
    //}
}