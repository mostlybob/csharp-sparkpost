using NUnit.Framework;

namespace SparkPost.Tests
{
    [TestFixture]
    public class TransmissionSenderTests
    {
        [Test]
        public void FireAnEmail()
        {
            var transmission = new SparkPost.TransmissionSender();
            var result = transmission.FireAnEmail("");
            result.Wait();
        }

        [Test]
        public void FireATemplate()
        {
            var transmission = new SparkPost.TransmissionSender();
            var result = transmission.FireATemplate("");
            result.Wait();
        }
    }
}