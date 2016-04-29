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

        [Test]
        public void It_should_handle_harder_strings()
        {
            SnakeCase.Convert("TestTesting").ShouldEqual("test_testing");
            SnakeCase.Convert("TestingTest").ShouldEqual("testing_test");
            SnakeCase.Convert("ApppppAppppppp").ShouldEqual("appppp_appppppp");
            SnakeCase.Convert("ApppppppAppppp").ShouldEqual("appppppp_appppp");
        }

        [Test]
        public void It_should_convert_null_to_null()
        {
            SnakeCase.Convert(null).ShouldBeNull();
        }
    }
}