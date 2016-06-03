using System;
using System.Configuration;

namespace SparkPost.Examples
{
    internal class BCC
    {
        public static void Main(string[] args)
        {
            var settings = ConfigurationManager.AppSettings;
            var fromAddr = settings["fromaddr"];
            var toAddr = settings["toaddr"];
            var ccAddr = settings["ccaddr"];
            var bccAddr = settings["bccaddr"];

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

            var bcc = new Recipient
            {
                Address = new Address
                {
                    Email = bccAddr,
                    HeaderTo = toAddr
                }
            };
            trans.Recipients.Add(bcc);

            trans.Content.From.Email = fromAddr;
            trans.Content.Subject = "SparkPost BCC / CC example";
            trans.Content.Text =
                "This message was sent To 1 recipient, 1 recipient was CC'd and 1 sneaky recipient was BCC'd.";
            trans.Content.Headers.Add("CC", ccAddr);

            Console.Write("Sending BCC / CC sample mail...");

            var client = new Client(settings["apikey"]);
            client.CustomSettings.SendingMode = SendingModes.Sync;

            var response = client.Transmissions.Send(trans);

            Console.WriteLine("done");
        }
    }
}