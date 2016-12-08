using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Moq;
using NUnit.Framework;
using Should;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Text;

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
        public class ParseTests
        {
            private MailMessage _msg;
            private Transmission _trans;

            [SetUp]
            public void Setup()
            {
                _msg = new MailMessage();
                _msg.From = new MailAddress("jim@example.com", "Jim Example");
                _msg.To.Add(new MailAddress("bob@example.com", "Bob Example"));
                _msg.CC.Add(new MailAddress("susan@example.com", "Susan Example"));
                _msg.Bcc.Add(new MailAddress("richard@example.com", "Richard Example"));
                _msg.ReplyToList.Add(new MailAddress("rebecca@example.com", "Rebecca Example"));
                _msg.Body = "Unit test message";
                _msg.Subject = "Test ssubject";



                _trans = new Transmission(_msg);                
            }

            
            [Test]
            public void From_should_match()
            {
                Assert.That(_trans.Content.From.Name, Is.EqualTo(_msg.From.DisplayName));
                Assert.That(_trans.Content.From.Email, Is.EqualTo(_msg.From.Address));
            }

            [Test]
            public void It_should_have_three_recipients()
            {
                Assert.That(_trans.Recipients.Count, Is.EqualTo(3));
            }

            [Test]
            public void To_should_match()
            {
                var to = _trans.Recipients.SingleOrDefault(r => r.Type == RecipientType.To);
                Assert.That(to, Is.Not.Null);
                Assert.That(to.Address.Name == _msg.To.First().DisplayName);
                Assert.That(to.Address.Email == _msg.To.First().Address);
            }

            [Test]
            public void Cc_should_match()
            {
                var cc = _trans.Recipients.SingleOrDefault(r => r.Type == RecipientType.CC);
                Assert.That(cc, Is.Not.Null);
                Assert.That(cc.Address.Name == _msg.CC.First().DisplayName);
                Assert.That(cc.Address.Email == _msg.CC.First().Address);
            }

            [Test]
            public void Bcc_should_match()
            {
                var bcc = _trans.Recipients.SingleOrDefault(r => r.Type == RecipientType.BCC);
                Assert.That(bcc, Is.Not.Null);
                Assert.That(bcc.Address.Name == _msg.Bcc.First().DisplayName);
                Assert.That(bcc.Address.Email == _msg.Bcc.First().Address);
            }

            [Test]
            public void Replyto_should_match()
            {
                Assert.That(_trans.Content.ReplyTo, Is.EqualTo(_msg.ReplyToList.First().Address));
            }

            [Test]
            public void Subject_should_match()
            {
                Assert.That(_trans.Content.Subject, Is.EqualTo(_msg.Subject));
            }

            [Test]
            public void Text_body_should_match()
            {
                Assert.That(_trans.Content.Text, Is.EqualTo(_msg.Body));
            }

            [Test]
            public void Html_body_should_match()
            {
                _msg.IsBodyHtml = true;
                _trans = new Transmission(_msg);
                Assert.That(_trans.Content.Html, Is.EqualTo(_msg.Body));
            }

            [Test]
            public void It_should_use_alternate_html_view()
            {
                var html = "<p>Html body</p>";
                var view = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);
                _msg.AlternateViews.Add(view);
                _trans = new Transmission(_msg);

                Assert.That(_trans.Content.Html, Is.EqualTo(html));
            }

            [Test]
            public void It_should_use_alternate_text_view()
            {
                _msg.IsBodyHtml = true;
                var text = "Text body";
                var view = AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain);
                _msg.AlternateViews.Add(view);
                _trans = new Transmission(_msg);

                Assert.That(_trans.Content.Text, Is.EqualTo(text));
            }

            [Test]
            public void It_should_use_both_alternate_views()
            {
                var text = "Alternate text";
                var html = "Alternate html";
                var view = AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain);
                _msg.AlternateViews.Add(view);
                view = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);
                _msg.AlternateViews.Add(view);
                _trans = new Transmission(_msg);

                Assert.That(_trans.Content.Text, Is.EqualTo(text));
                Assert.That(_trans.Content.Html, Is.EqualTo(html));
            }

            [Test]
            public void It_should_copy_attachments()
            {
                var text = "This is an attachment";
                var name = "foo.txt";
                var type = "text/plain";
                var ms = new MemoryStream(Encoding.ASCII.GetBytes(text));
                _msg.Attachments.Add(new System.Net.Mail.Attachment(ms, name, type));
                _trans = new Transmission(_msg);

                Assert.That(_trans.Content.Attachments.Count, Is.EqualTo(1));
                Assert.That(_trans.Content.Attachments.First().Type, Is.EqualTo(type));
                Assert.That(_trans.Content.Attachments.First().Name, Is.EqualTo(name));

                var bytes = Convert.FromBase64String(_trans.Content.Attachments.First().Data);
                var decoded = Encoding.ASCII.GetString(bytes);
                Assert.That(decoded, Is.EqualTo(text));
            }
        }
    }
}