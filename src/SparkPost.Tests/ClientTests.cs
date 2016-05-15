using System.Net.Http;
using NUnit.Framework;
using Should;

namespace SparkPost.Tests
{
    public class ClientTests
    {
        [TestFixture]
        public class HttpClientOverridingTests
        {
            [SetUp]
            public void Setup()
            {
                client = new Client(null);
            }

            private Client client;

            [Test]
            public void By_default_it_should_return_new_http_clients_each_time()
            {
                var first = client.CustomSettings.CreateANewHttpClient();
                var second = client.CustomSettings.CreateANewHttpClient();

                first.ShouldNotBeNull();
                second.ShouldNotBeNull();
                first.ShouldNotBeSameAs(second);
            }

            [Test]
            public void It_should_allow_the_overriding_of_the_http_client_building()
            {
                var httpClient = new HttpClient();

                client.CustomSettings.BuildHttpClientsUsing(() => httpClient);

                client.CustomSettings.CreateANewHttpClient().ShouldBeSameAs(httpClient);
                client.CustomSettings.CreateANewHttpClient().ShouldBeSameAs(httpClient);
                client.CustomSettings.CreateANewHttpClient().ShouldBeSameAs(httpClient);
                client.CustomSettings.CreateANewHttpClient().ShouldBeSameAs(httpClient);
            }

            [Test]
            public void It_should_default_to_async()
            {
                client.CustomSettings.SendingMode.ShouldEqual(SendingModes.Async);
            }

            [Test]
            public void it_should_have_inbound_domains()
            {
                client.InboundDomains.ShouldNotBeNull();
            }
        }
    }
}