<a href="https://www.sparkpost.com"><img src="https://www.sparkpost.com/sites/default/files/attachments/SparkPost_Logo_2-Color_Gray-Orange_RGB.svg" width="200px"/></a>

[Sign up](https://app.sparkpost.com/sign-up?src=Dev-Website&sfdcid=70160000000pqBb) for a SparkPost account and visit our [Developer Hub](https://developers.sparkpost.com) for even more content.

# SparkPost C# Library

The official C# package for the [SparkPost API](https://www.sparkpost.com/api).

## Installation

This library can be found on Nuget at ?

Alternatively, you can get the latest dll from the releases tab.  You can also download this code and compile it yourself.

## Usage

To send an email:

```c#
var transmission = new Transmission();
transmission.Content.From.Email = "no-reply@myemail.com";
transmission.Content.Subject = "My Email Subject";
transmission.Content.Text = "my text content";
transmission.Content.Html = "<b>My HTML Content</b>";

var recipient = new Recipient
{
    Address = new Address { Email = "my@email.com" }
};
transmissionission.Recipients.Add(recipient);

var client = new Client("MY_API_KEY");
client.Transmissions.Send(transmission)

```

To send a template email:

```c#
var transmission = new Transmission();
transmission.Content.TemplateId = "my-template-id";
transmission.Content.From.Email = "no-reply@myemail.com";

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

transmissionission.Recipients.Add(recipient);

var client = new Client("MY_API_KEY);
client.Transmissions.Send(transmission)

```

### Contribute

We welcome your contributions!  See [CONTRIBUTING.md](CONTRIBUTING.md) for details on how to help out.

### Change Log

[See ChangeLog here](CHANGELOG.md)
