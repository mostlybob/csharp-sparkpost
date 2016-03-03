using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;

namespace SparkPost.Tests
{
    public class DataMapperTests
    {
        [TestFixture]
        public class TransmissionMappingTests
        {
            [SetUp]
            public void Setup()
            {
                transmission = new Transmission();
                mapper = new DataMapper("v1");
            }

            private Transmission transmission;
            private DataMapper mapper;

            [Test]
            public void It_should_set_the_content_dictionary()
            {
                var email = Guid.NewGuid().ToString();
                transmission.Content.From = new Address {Email = email};
                mapper.ToDictionary(transmission)["content"]
                    .CastAs<IDictionary<string, object>>()["from"].ShouldEqual(email);
            }

            [Test]
            public void It_should_set_the_recipients()
            {
                var recipient1 = new Recipient {Address = new Address {Email = Guid.NewGuid().ToString()}};
                var recipient2 = new Recipient {Address = new Address {Email = Guid.NewGuid().ToString()}};

                transmission.Recipients = new List<Recipient> {recipient1, recipient2};

                var result = mapper.ToDictionary(transmission)["recipients"] as IEnumerable<IDictionary<string, object>>;
                result.Count().ShouldEqual(2);
                result.ToList()[0]["address"].ShouldBeSameAs(recipient1.Address.Email);
                result.ToList()[1]["address"].ShouldBeSameAs(recipient2.Address.Email);
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