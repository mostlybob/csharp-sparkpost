using System.Collections;
using System.Collections.Generic;

namespace SparkPost
{
    public class Recipient
    {
        public Address Address { get; set; }
        public string ReturnPath { get; set; }
        public IList Tags { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public IDictionary<string, string> SubstitutionData { get; set; }
    }
}