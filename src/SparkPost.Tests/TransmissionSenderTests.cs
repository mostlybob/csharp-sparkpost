using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SparkPost.Tests
{

    [TestFixture]
    public class TransmissionsTests
    {
        private string apiKey;
        private string apiHost;
        private Client client;

        [SetUp]
        public void Setup()
        {
            client = new Client("9eeefef17e9852279cce57c44221bbbe450305c9");
        }

        [Test]
        public void It_should_be_able_to_send_an_email()
        {
            var transmission = new Transmission
            {
                Content =
                {
                    From = new Address {Email = "testing@sparkpostbox.com"},
                    TemplateId = "my-first-email"
                }
            };

            transmission.Recipients.Add(new Recipient {Address = new Address {Email = "darren@cauthon.com"}});

            var result = client.Transmissions.Send(transmission);
            result.Wait();
        }
    }
   
}