using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SparkPost.Acceptance
{
    [Binding]
    public class TransmissionSteps
    {
        private readonly ScenarioContext scenarioContext;

        public TransmissionSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I have a new transmission")]
        public void GivenIHaveANewTransmissionWith()
        {
            var transmission = new Transmission();
            scenarioContext.Set(transmission);
        }

        [Given(@"the transmission is meant to be sent from '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeSentfrom(string email)
        {
            var transmission = scenarioContext.Get<Transmission>();
            transmission.Content.From = new Address {Email = email};
            scenarioContext.Set(transmission);
        }

        [Given(@"the transmission is meant to be sent to '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeSentTo(string email)
        {
            var transmission = scenarioContext.Get<Transmission>();
            transmission.Recipients.Add(new Recipient {Address = new Address {Email = email}});
            scenarioContext.Set(transmission);
        }

        [Given(@"the transmission content is")]
        public void GivenTheTransmissionContentIs(Table table)
        {
            var transmission = scenarioContext.Get<Transmission>();
            table.FillInstance(transmission.Content);
            scenarioContext.Set(transmission);
        }

        [Given(@"the transmission template id is set to '(.*)'")]
        public void x(string templateId)
        {
            var transmission = scenarioContext.Get<Transmission>();

            transmission.Content.TemplateId = templateId;

            scenarioContext.Set(transmission);
        }

        [Given(@"the transmission has a text file attachment")]
        public void GivenTheTransmissionHasATextFileAttachment()
        {
            var transmission = scenarioContext.Get<Transmission>();

            var attachment = File.Create<Attachment>("testtextfile.txt");

            transmission.Content.Attachments.Add(attachment);

            scenarioContext.Set(transmission);
        }

        [Given(@"the transmission is meant to be CCd to '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeCCdTo(string email)
        {
            var transmission = scenarioContext.Get<Transmission>();

            transmission.Recipients.Add(new Recipient
            {
                Type = RecipientType.CC,
                Address = new Address {Email = email}
            });

            scenarioContext.Set(transmission);
        }

        [Given(@"the transmission is meant to be BCCd to '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeBCCdTo(string email)
        {
            var transmission = scenarioContext.Get<Transmission>();

            transmission.Recipients.Add(new Recipient
            {
                Type = RecipientType.BCC,
                Address = new Address {Email = email}
            });

            scenarioContext.Set(transmission);
        }

        [When(@"I send the transmission")]
        public void WhenISendTheTransmission()
        {
            var client = scenarioContext.Get<IClient>();
            var transmission = scenarioContext.Get<Transmission>();

            SendTransmissionResponse response = null;

            Task.Run(async () => { response = await client.Transmissions.Send(transmission); }).Wait();

            scenarioContext.Set(response);
            scenarioContext.Set<Response>(response);
        }
    }
}