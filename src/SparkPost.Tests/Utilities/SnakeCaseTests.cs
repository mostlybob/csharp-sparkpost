using NUnit.Framework;
using Should;
using SparkPost.Utilities;

namespace SparkPost.Tests.Utilities
{
    [TestFixture]
    public class SnakeCaseTests
    {
        [Test]
        public void It_should_convert_things_to_snake_case()
        {
            SnakeCase.Convert("T").ShouldEqual("t");
            SnakeCase.Convert("Test").ShouldEqual("test");
            SnakeCase.Convert("TEST").ShouldEqual("t_e_s_t");
            SnakeCase.Convert("JohnGalt").ShouldEqual("john_galt");
        }
    }
}