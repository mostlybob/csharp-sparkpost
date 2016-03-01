using NUnit.Framework;

namespace SparkPost.Tests
{
    [TestFixture]
    public class TransmissionTests
    {
        [Test]
        public void FireAnEmail()
        {
            var transmission = new Transmission();
            var result = transmission.FireAnEmail("");
            result.Wait();
        }

        [Test]
        public void FireATemplate()
        {
            var transmission = new Transmission();
            var result = transmission.FireATemplate("");
            result.Wait();
        }
    }
}