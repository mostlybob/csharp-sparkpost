using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost.Tests
{
    [TestFixture]
    public class MetricsQueryTests
    {
        private MetricsQuery _query;

        [SetUp]
        public void Setup()
        {
            _query = new MetricsQuery();
        }

        private void Check(IList<string> list)
        {
            Assert.That(list, Is.Not.Null);
            Assert.That(list, Is.Empty);
        }

        [Test]
        public void It_should_have_a_default_campaigns_list() => Check(_query.Campaigns);

        [Test]
        public void It_should_have_a_default_domains_list() => Check(_query.Domains);

        [Test]
        public void It_should_have_a_default_metrics_list() => Check(_query.Metrics);

        [Test]
        public void It_should_have_a_default_templates_list() => Check(_query.Templates);

        [Test]
        public void It_should_have_a_default_sending_ips_list() => Check(_query.SendingIps);

        [Test]
        public void It_should_have_a_default_ip_pools_list() => Check(_query.IpPools);

        [Test]
        public void It_should_have_a_default_sending_domains_list() => Check(_query.SendingDomains);

        [Test]
        public void It_should_have_a_default_subaccounts_list() => Check(_query.Subaccounts);
    }
}
