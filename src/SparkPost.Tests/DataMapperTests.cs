using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Should;

namespace SparkPost.Tests
{
    public class DataMapperTests
    {
        [TestFixture]
        public class TransmissionMappingTests
        {
            private Transmission transmission;
            private DataMapper mapper;

            [SetUp]
            public void Setup()
            {
                transmission = new Transmission();
                mapper = new DataMapper(null);
            }

            [Test]
            public void It_should_set_the_content_dictionary()
            {
                var content = new Mock<Content>();
                var contentDictionary = new Dictionary<string, object>();
                content.Setup(x => x.ToDictionary())
                    .Returns(contentDictionary);

                transmission.Content = content.Object;
                mapper.ToDictionary(transmission)["content"]
                    .ShouldBeSameAs(contentDictionary);
            }

            [Test]
            public void It_should_set_the_recipients()
            {
                var recipient1 = new Mock<Recipient>();
                var recipient1Dictionary = new Dictionary<string, object>();
                recipient1.Setup(x => x.ToDictionary()).Returns(recipient1Dictionary);

                var recipient2 = new Mock<Recipient>();
                var recipient2Dictionary = new Dictionary<string, object>();
                recipient2.Setup(x => x.ToDictionary()).Returns(recipient2Dictionary);

                var recipients = new List<Recipient>
                {
                    recipient1.Object, 
                    recipient2.Object,
                };

                transmission.Recipients = recipients;

                var result = mapper.ToDictionary(transmission)["recipients"] as IEnumerable<IDictionary<string, object>>;
                result.Count().ShouldEqual(2);
                result.ToList()[0].ShouldBeSameAs(recipient1Dictionary);
                result.ToList()[1].ShouldBeSameAs(recipient2Dictionary);
            }

            [Test]
            public void It_should_set_the_recipients_to_a_list_id_if_a_list_id_is_provided()
            {
                var listId = Guid.NewGuid().ToString();
                transmission.ListId = listId;

                var result = mapper.ToDictionary(transmission)["recipients"] as IDictionary<string, object>;
                result["list_id"].ShouldEqual(listId);
            }

        }
    }
}
