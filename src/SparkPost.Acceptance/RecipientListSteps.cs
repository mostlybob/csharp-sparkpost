using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SparkPost.Acceptance
{
    [Binding]
    public class RecipientListSteps
    {
        [Given(@"I have a new recipient list as")]
        public void GivenIHaveANewRecipientListAs(Table table)
        {
            var recipientList = table.CreateInstance<RecipientList>();
            ScenarioContext.Current.Set(recipientList);
        }

        [Given(@"I add '(.*)' to the recipient list")]
        public void GivenIAddToTheRecipientList(string email)
        {
            var recipientList = ScenarioContext.Current.Get<RecipientList>();
            recipientList.Recipients.Add(new Recipient {Address = new Address {Email = email}});
        }

        [Given(@"I do not have a recipient list of id '(.*)'")]
        public void GivenIDoNotHaveARecipientListOfId(string id)
        {
            var client = ScenarioContext.Current.Get<IClient>();
            client.RecipientLists.Delete(id);
        }

        [When(@"I create the recipient list")]
        public void WhenICreateTheRecipientList()
        {
            var recipientList = ScenarioContext.Current.Get<RecipientList>();

            var client = ScenarioContext.Current.Get<IClient>();

            SendRecipientListsResponse response = null;
            Task.Run(async () => { response = await client.RecipientLists.Create(recipientList); }).Wait();

            ScenarioContext.Current.Set(response);
            ScenarioContext.Current.Set<Response>(response);
        }

        [When(@"I retrieve the ""(.*)"" recipient list")]
        public void WhenIRetrieveTheRecipientList(string key)
        {
            var client = ScenarioContext.Current.Get<IClient>();

            RetrieveRecipientListsResponse response = null;

            Task.Run(async () =>
            {
                response = await client.RecipientLists.Retrieve(key);
            }).Wait();

            ScenarioContext.Current.Set(response);
            ScenarioContext.Current.Set<Response>(response);
        }
    }
}