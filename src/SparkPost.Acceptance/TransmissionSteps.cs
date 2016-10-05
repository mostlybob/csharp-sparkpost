using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SparkPost.Acceptance
{
    [Binding]
    public class TransmissionSteps
    {
        [Given(@"I have a new transmission")]
        public void GivenIHaveANewTransmissionWith()
        {
            var transmission = new Transmission();
            ScenarioContext.Current.Set(transmission);
        }

        [Given(@"the transmission is meant to be sent from '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeSentfrom(string email)
        {
            var transmission = ScenarioContext.Current.Get<Transmission>();
            transmission.Content.From = new Address {Email = email};
            ScenarioContext.Current.Set(transmission);
        }

        [Given(@"the transmission is meant to be sent to '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeSentTo(string email)
        {
            var transmission = ScenarioContext.Current.Get<Transmission>();
            transmission.Recipients.Add(new Recipient {Address = new Address {Email = email}});
            ScenarioContext.Current.Set(transmission);
        }

        [Given(@"the transmission content is")]
        public void GivenTheTransmissionContentIs(Table table)
        {
            var transmission = ScenarioContext.Current.Get<Transmission>();
            table.FillInstance(transmission.Content);
            ScenarioContext.Current.Set(transmission);
        }

        [When(@"I send the transmission")]
        public void WhenISendTheTransmission()
        {
            var client = ScenarioContext.Current.Get<IClient>();
            var transmission = ScenarioContext.Current.Get<Transmission>();

            SendTransmissionResponse response = null;

            Task.Run(async () => { response = await client.Transmissions.Send(transmission); }).Wait();

            ScenarioContext.Current.Set(response);
            ScenarioContext.Current.Set<Response>(response);
        }
    }
}