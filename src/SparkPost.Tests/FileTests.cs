using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPost.Tests
{
    [TestFixture]
    public class FileTests
    {
        [Test]
        public void It_should_create_correct_type()
        {
            byte[] content = null;
            Assert.That(File.Create<Attachment>(content), Is.InstanceOf<Attachment>());
            Assert.That(File.Create<InlineImage>(content), Is.InstanceOf<InlineImage>());
        }

        [TestCase("This is some test data.")]
        [TestCase("This is some other data.")]
        public void It_should_encode_data_correctly(string s)
        {
            var b = GetBytes(s);
            var attach = File.Create<Attachment>(b);
            Assert.That(attach.Data, Is.EqualTo(EncodeString(s)));
        }

        [TestCase("foo.png", "image/png")]
        [TestCase("foo.txt", "text/plain")]
        [TestCase("sf", "application/octet-stream")]
        [TestCase("", "application/octet-stream")]
        public void It_should_set_name_and_type_correctly(string filename, string mimeType)
        {
            var b = GetBytes("Some Test Data");
            var attach = File.Create<Attachment>(b, filename);
            Assert.That(attach.Name, Is.EqualTo(filename));
            Assert.That(attach.Type, Is.EqualTo(mimeType));
        }

        private byte[] GetBytes(string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }

        private string EncodeString(string input)
        {
            return Convert.ToBase64String(GetBytes(input));
        }

    }
}
