using System;
using System.Collections.Generic;
using System.Configuration;

namespace SparkPost.Examples
{
    internal class Order
    {
        public int OrderId { get; set; }
        public string Desc { get; set; }
        public int Total { get; set; }
    }

    internal class SendTemplate
    {
        public static void Main(string[] args)
        {
            var settings = ConfigurationManager.AppSettings;
            var fromAddr = settings["fromaddr"];
            var toAddr = settings["toaddr"];

            var trans = new Transmission();

            var to = new Recipient
            {
                Address = new Address
                {
                    Email = toAddr
                },
                SubstitutionData = new Dictionary<string, object>
                {
                    {"firstName", "Jane"}
                }
            };

            trans.Recipients.Add(to);
            trans.SubstitutionData["title"] = "Dr";
            trans.SubstitutionData["firstName"] = "Rick";
            trans.SubstitutionData["lastName"] = "Sanchez";
            trans.SubstitutionData["orders"] = new List<Order>
            {
                new Order {OrderId = 101, Desc = "Tomatoes", Total = 5},
                new Order {OrderId = 271, Desc = "Entropy", Total = 314}
            };

            trans.Content.From.Email = fromAddr;
            trans.Content.TemplateId = "orderSummary";

            Console.Write("Sending mail...");

            var client = new Client(settings["apikey"]);
            client.CustomSettings.SendingMode = SendingModes.Sync;

            var response = client.Transmissions.Send(trans);

            Console.WriteLine("done");
        }
    }
}