using System.Collections;
using System.Collections.Generic;

namespace SparkPost
{
    public class Recipient
    {
        public Recipient()
        {
            Address = new Address();
            Tags = new List<string>();
        }

        public Address Address { get; set; }
        public string ReturnPath { get; set; }
        public IList<string> Tags { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public IDictionary<string, string> SubstitutionData { get; set; }
    }
}