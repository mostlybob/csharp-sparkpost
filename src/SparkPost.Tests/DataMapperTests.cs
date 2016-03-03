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

            [Test]
            public void campaign_id()
            {
                var value = Guid.NewGuid().ToString();
                transmission.CampaignId = value;
                mapper.ToDictionary(transmission)["campaign_id"].ShouldEqual(value);
            }

            [Test]
            public void description()
            {
                var value = Guid.NewGuid().ToString();
                transmission.Description = value;
                mapper.ToDictionary(transmission)["description"].ShouldEqual(value);
            }

            [Test]
            public void return_path()
            {
                var value = Guid.NewGuid().ToString();
                transmission.ReturnPath = value;
                mapper.ToDictionary(transmission)["return_path"].ShouldEqual(value);
            }

            [Test]
            public void do_not_send_the_return_path_if_it_is_not_provided()
            {
                mapper.ToDictionary(transmission).Keys.ShouldNotContain("return_path");
            }

            [Test]
            public void metadata()
            {
                var key = Guid.NewGuid().ToString();
                var value = Guid.NewGuid().ToString();
                transmission.Metadata[key] = value;
                mapper.ToDictionary(transmission)["metadata"]
                    .CastAs<IDictionary<string, string>>()[key].ShouldEqual(value);
            }

            [Test]
            public void do_not_include_empty_metadata()
            {
                mapper.ToDictionary(transmission).Keys.ShouldNotContain("metadata");
            }

            [Test]
            public void substitution_data()
            {
                var key = Guid.NewGuid().ToString();
                var value = Guid.NewGuid().ToString();
                transmission.SubstitutionData[key] = value;
                mapper.ToDictionary(transmission)["substitution_data"]
                    .CastAs<IDictionary<string, string>>()[key].ShouldEqual(value);
            }

            [Test]
            public void do_not_include_empty_substitution_data()
            {
                mapper.ToDictionary(transmission).Keys.ShouldNotContain("substitution_data");
            }

            [Test]
            public void options()
            {
                transmission.Options.ClickTracking = true;
                mapper.ToDictionary(transmission)["options"]
                    .CastAs<IDictionary<string, object>>()
                    ["click_tracking"].ShouldEqual("true");

                transmission.Options.ClickTracking = false;
                mapper.ToDictionary(transmission)["options"]
                    .CastAs<IDictionary<string, object>>()
                    ["click_tracking"].ShouldEqual("false");
            }
        }

        [TestFixture]
        public class ContentMappingTests
        {
            [SetUp]
            public void Setup()
            {
                content = new Content();
                mapper = new DataMapper("v1");
            }

            private Content content;
            private DataMapper mapper;

            [Test]
            public void from()
            {
                var email = Guid.NewGuid().ToString();
                content.From.Email = email;
                mapper.ToDictionary(content)["from"].ShouldEqual(email);
            }

            [Test]
            public void subject()
            {
                var value = Guid.NewGuid().ToString();
                content.Subject = value;
                mapper.ToDictionary(content)["subject"].ShouldEqual(value);
            }

            [Test]
            public void text()
            {
                var value = Guid.NewGuid().ToString();
                content.Text = value;
                mapper.ToDictionary(content)["text"].ShouldEqual(value);
            }

            [Test]
            public void template_id()
            {
                var value = Guid.NewGuid().ToString();
                content.TemplateId = value;
                mapper.ToDictionary(content)["template_id"].ShouldEqual(value);
            }

            [Test]
            public void html()
            {
                var value = Guid.NewGuid().ToString();
                content.Html = value;
                mapper.ToDictionary(content)["html"].ShouldEqual(value);
            }

            [Test]
            public void reply_to()
            {
                var value = Guid.NewGuid().ToString();
                content.ReplyTo = value;
                mapper.ToDictionary(content)["reply_to"].ShouldEqual(value);
            }

            [Test]
            public void headers()
            {
                var key = Guid.NewGuid().ToString();
                var value = Guid.NewGuid().ToString();
                content.Headers[key] = value;
                mapper.ToDictionary(content)["headers"]
                    .CastAs<IDictionary<string, string>>()
                    [key].ShouldEqual(value);
            }

            [Test]
            public void do_not_include_empty_headers()
            {
                mapper.ToDictionary(content)
                    .Keys.ShouldNotContain("headers");
            }
        }
    }
}