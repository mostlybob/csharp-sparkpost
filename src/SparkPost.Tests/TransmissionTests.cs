using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Moq;
using NUnit.Framework;
using Should;

namespace SparkPost.Tests
{
    public class TransmissionTests
    {
        [TestFixture]
        public class ConstructorTests
        {
            [SetUp]
            public void Setup()
            {
                transmission = new Transmission();
            }

            private Transmission transmission;

            [Test]
            public void It_should_not_be_missing_a_recipients_list()
            {
                transmission.Recipients.ShouldNotBeNull();
            }

            [Test]
            public void It_should_not_be_missing_content()
            {
                transmission.Content.ShouldNotBeNull();
            }

            [Test]
            public void It_should_not_be_missing_metadata()
            {
                transmission.Metadata.ShouldNotBeNull();
            }

            [Test]
            public void It_should_not_be_missing_substition_data()
            {
                transmission.SubstitutionData.ShouldNotBeNull();
            }
        }

        [TestFixture]
        public class ToDictionaryTests
        {
            private Transmission transmission;

            [SetUp]
            public void Setup()
            {
                transmission = new Transmission();
            }

            [Test]
            public void It_should_set_the_content_dictionary()
            {
                var content = new Mock<Content>();
                var contentDictionary = new Dictionary<string, object>();
                content.Setup(x => x.ToDictionary())
                    .Returns(contentDictionary);

                transmission.Content = content.Object;
                transmission.ToDictionary()["content"]
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

                var result = transmission.ToDictionary()["recipients"] as IEnumerable<IDictionary<string, object>>;
                result.Count().ShouldEqual(2);
                result.ToList()[0].ShouldBeSameAs(recipient1Dictionary);
                result.ToList()[1].ShouldBeSameAs(recipient2Dictionary);
            }

            [Test]
            public void It_should_set_the_recipients_to_a_list_id_if_a_list_id_is_provided()
            {
                var listId = Guid.NewGuid().ToString();
                transmission.ListId = listId;

                var result = transmission.ToDictionary()["recipients"] as IDictionary<string, object>;
                result["list_id"].ShouldEqual(listId);
            }

        }
    }
}