using System;
using System.Configuration;

using SparkPost;

namespace SparkPost.Examples
{
	class BCC
	{
		public static void Main(string[] args)
		{
			var settings = ConfigurationManager.AppSettings;
			String fromAddr = settings["fromaddr"];
			String toAddr = settings["toaddr"];
			String ccAddr = settings["ccaddr"];
			String bccAddr = settings["bccaddr"];

			var trans = new Transmission();

			var to = new Recipient()
			{
				Address = new Address { Email = toAddr }
			};
			trans.Recipients.Add(to);

			var cc = new Recipient()
			{
				Address = new Address
				{
					Email = ccAddr,
					HeaderTo = toAddr
				}
			};
			trans.Recipients.Add(cc);

			var bcc = new Recipient()
			{
				Address = new Address()
				{
					Email = bccAddr,
					HeaderTo = toAddr
				}
			};
			trans.Recipients.Add(bcc);

			trans.Content.From.Email = fromAddr;
			trans.Content.Subject = "SparkPost BCC / CC example";
			trans.Content.Text = "This message was sent To 1 recipient, 1 recipient was CC'd and 1 sneaky recipient was BCC'd.";
			trans.Content.Headers.Add("CC", ccAddr);

			Console.Write("Sending BCC / CC sample mail...");
			new Client(settings["apikey"]).Transmissions.Send(trans).Wait();
			Console.WriteLine("done");
		}
	}
}
