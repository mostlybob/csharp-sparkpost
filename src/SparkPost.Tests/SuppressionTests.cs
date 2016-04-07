using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.XPath;
using AutoMoq.Helpers;
using Moq;
using NUnit.Framework;
using Should;

namespace SparkPost.Tests
{
    public class SuppressionTests
    {
        [TestFixture]
        public class DeleteTests : AutoMoqTestFixture<Suppressions>
        {
            private Response response;
            private string email;

            [SetUp]
            public void Setup()
            {
                ResetSubject();

                response = new Response {StatusCode = HttpStatusCode.NoContent};

                Mocked<IRequestSender>()
                    .Setup(x => x.Send(It.IsAny<Request>()))
                    .Returns(Task.FromResult(response));

                email = Guid.NewGuid().ToString();
            }

            [Test]
            public async void It_should_return_true_if_the_web_request_returns_no_content()
            {
                var result = await Subject.Delete(email);
                result.ShouldBeTrue();
            }

            [Test]
            public async void It_should_return_false_if_the_web_request_returns_anything_but_no_content()
            {
                response.StatusCode = HttpStatusCode.Accepted;
                (await Subject.Delete(email)).ShouldBeFalse();

                response.StatusCode = HttpStatusCode.Ambiguous;
                (await Subject.Delete(email)).ShouldBeFalse();

                response.StatusCode = HttpStatusCode.UpgradeRequired;
                (await Subject.Delete(email)).ShouldBeFalse();
            }
        }
    }
}