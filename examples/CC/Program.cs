using System;
using System.Configuration;

namespace SparkPost.Examples
{
    internal class CC
    {
        public static void Main(string[] args)
        {
            var settings = ConfigurationManager.AppSettings;
            var fromAddr = settings["fromaddr"];
            var toAddr = settings["toaddr"];
            var ccAddr = settings["ccaddr"];

            var trans = new Transmission();

            var to = new Recipient
            {
                Address = new Address {Email = toAddr}
            };
            trans.Recipients.Add(to);

            var cc = new Recipient
            {
                Address = new Address
                {
                    Email = ccAddr,
                    HeaderTo = toAddr
                }
            };
            trans.Recipients.Add(cc);

            trans.Content.From.Email = fromAddr;
            trans.Content.Subject = "SparkPost CC example";
            trans.Content.Text = "This message was sent To 1 recipient and 1 recipient was CC'd.";
            trans.Content.Headers.Add("CC", ccAddr);

            Console.Write("Sending CC sample mail...");

            var client = new Client(settings["apikey"]);
            client.CustomSettings.SendingMode = SendingModes.Sync;

            var response = client.Transmissions.Send(trans);

            Console.WriteLine("done");
        }
    }
}