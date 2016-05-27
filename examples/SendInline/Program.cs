using System;
using System.Configuration;
using System.Collections.Generic;

using SparkPost;

namespace SparkPost.Examples
{
	class SendInline
	{
		public static void Main(string[] args)
		{
			var settings = ConfigurationManager.AppSettings;
			String fromAddr = settings["fromaddr"];
			String toAddr = settings["toaddr"];

			var trans = new Transmission();

			var to = new Recipient()
			{
				Address = new Address
				{
					Email = toAddr
				},
				SubstitutionData = new Dictionary<string, object>()
				{
					{ "firstName", "Jane" }
				}
			};

			trans.Recipients.Add(to);

			trans.SubstitutionData["firstName"] = "Oh Ye Of Little Name";

			trans.Content.From.Email = fromAddr;
			trans.Content.Subject = "SparkPost online content example";
			trans.Content.Text = "Greetings {{firstName or 'recipient'}}\nHello from C# land.";
			trans.Content.Html = "<html><body><h2>Greetings {{firstName or 'recipient'}}</h2><p>Hello from C# land.</p></body></html>";

			Console.Write("Sending mail...");
			new Client(settings["apikey"]).Transmissions.Send(trans).Wait();
			Console.WriteLine("done");
		}
	}
}
