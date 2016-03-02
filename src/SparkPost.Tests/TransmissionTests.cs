using System.Collections.Generic;
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

        }
    }
}