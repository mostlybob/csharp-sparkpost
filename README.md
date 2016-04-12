<a href="https://www.sparkpost.com"><img src="https://www.sparkpost.com/sites/default/files/attachments/SparkPost_Logo_2-Color_Gray-Orange_RGB.svg" width="200px"/></a>

[Sign up](https://app.sparkpost.com/sign-up?src=Dev-Website&sfdcid=70160000000pqBb) for a SparkPost account and visit our [Developer Hub](https://developers.sparkpost.com) for even more content.

# SparkPost C# Library

[![Travis CI](https://travis-ci.org/SparkPost/ruby-sparkpost.svg?branch=master)](https://travis-ci.org/SparkPost/csharp-sparkpost)  [![Slack Status](http://slack.sparkpost.com/badge.svg)](http://slack.sparkpost.com)

The official C# package for the [SparkPost API](https://www.sparkpost.com/api). Xamarin.iOS and Xamarin.Android support provided in the Portable Package (PCL Profile7).

## Installation

To install via NuGet, run the following command in the [Package Manager Console](http://docs.nuget.org/consume/package-manager-console):

```
PM> Install-Package SparkPost
```

Alternatively, you can get the latest dll from the releases tab.  You can also download this code and compile it yourself.

## Usage

#### Special Note about ```Async```

This library uses .Net 4.5's ```Async``` functionality for better performance  ([read more here](https://msdn.microsoft.com/en-us/library/hh191443.aspx)).  This means that if you do not intend to support this ```async``` feature, you'll need to make one slight change to your calls:  Tack on ```.Wait()``` like so:

```c#
client.Transmissions.Send(transmission).Wait();
```

This will force the thread to wait until the web request has completed.  If you are calling this from an ```async``` method or if you use ```await```, no changes will be needed.

### Transmissions

To send an email:

```c#
var transmission = new Transmission();
transmission.Content.From.Email = "testing@sparkpostbox.com";;
transmission.Content.Subject = "Oh hey!";
transmission.Content.Text = "Testing SparkPost - the world\'s most awesomest email service!";
transmission.Content.Html = "<html><body><p>Testing SparkPost - the world\'s most awesomest email service!</p></body></html>";

var recipient = new Recipient
{
    Address = new Address { Email = "my@email.com" }
};
transmission.Recipients.Add(recipient);

var client = new Client("<YOUR API KEY>");
client.Transmissions.Send(transmission);
// or client.Transmissions.Send(transmission).Wait();

```

To send a template email:

```c#
var transmission = new Transmission();
transmission.Content.TemplateId = "my-template-id";
transmission.Content.From.Email = "testing@sparkpostbox.com";

transmission.SubstitutionData["first_name"] = "John";
transmission.SubstitutionData["last_name"] = "Doe";

var orders = new List<Order>
{
    new Order { OrderId = "1", Total = 101 },
    new Order { OrderId = "2", Total = 304 }
};

// you can pass more complicated data, so long as it
// can be parsed easily to JSON
transmission.SubstitutionData["orders"] = orders;

var recipient = new Recipient
{
    Address = new Address { Email = "my@email.com" }
};
transmission.Recipients.Add(recipient);

var client = new Client("MY_API_KEY");
client.Transmissions.Send(transmission);
// or client.Transmissions.Send(transmission).Wait();

```


### Suppression List

The suppression list are users who have opted-out of your emails.  To retrieve this list:

```c#
var client = new Client("MY_API_KEY");

client.Suppressions.List(); // returns a list of 

client.Suppressions.List(new { limit = 3 }); // it accepts an anonymous type for filters

client.Suppressions.List(new SuppressionQuery()); // a SuppressionQuery is also allowed for typed help
```

To add email addresses to the list:

```c#
var client = new Client("MY_API_KEY");

var item1 = new Suppression { Email = "testing@testing.com", NonTransactional = true };
var item2 = new Suppression { Email = "testing2@testing.com", Description = "testing" };

client.Suppressions.CreateOrUpdate(new []{ item1, item2 });
```

To delete email addresses from the list:

```c#
var client = new Client("MY_API_KEY");

client.Suppressions.Delete("testing@testing.com");
```

To retrieve details about an email address on (or not on) the list:

```c#
var client = new Client("MY_API_KEY");

client.Suppressions.Retrieve("testing@testing.com");
```


### Contribute

We welcome your contributions!  See [CONTRIBUTING.md](CONTRIBUTING.md) for details on how to help out.

### Change Log

[See ChangeLog here](CHANGELOG.md)
