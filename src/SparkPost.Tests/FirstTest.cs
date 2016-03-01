using NUnit.Framework;
using Should;

namespace SparkPost.Tests
{
    [TestFixture]
    public class FirstTest
    {
        [Test]
        public void True()
        {
            1.ShouldEqual(1);
        }
    }
}