using System.Collections.Generic;

namespace SparkPost
{
    public class Webhook
    {
        public Webhook()
        {
            Events = new List<string>();
        }

        public string Name { get; set; }
        public string Target { get; set; }
        public IList<string> Events { get; set; }
    }
}