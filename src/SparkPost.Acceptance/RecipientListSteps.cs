using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SparkPost.Acceptance
{
    [Binding]
    public class RecipientListSteps
    {
        private readonly ScenarioContext scenarioContext;

        public RecipientListSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I have a new recipient list as")]
        public void GivenIHaveANewRecipientListAs(Table table)
        {
            var recipientList = table.CreateInstance<RecipientList>();
            scenarioContext.Set(recipientList);
        }

        [Given(@"I add '(.*)' to the recipient list")]
        public void GivenIAddToTheRecipientList(string email)
        {
            var recipientList = scenarioContext.Get<RecipientList>();
            recipientList.Recipients.Add(new Recipient {Address = new Address {Email = email}});
        }

        [Given(@"I clear the recipients on the recipient list")]
        public void x()
        {
            var recipientList = scenarioContext.Get<RecipientList>();
            recipientList.Recipients.Clear();
        }

        [Given(@"I do not have a recipient list of id '(.*)'")]
        public void GivenIDoNotHaveARecipientListOfId(string id)
        {
            var client = scenarioContext.Get<IClient>();
            client.RecipientLists.Delete(id);
        }

        [When(@"I create the recipient list")]
        public void WhenICreateTheRecipientList()
        {
            var recipientList = scenarioContext.Get<RecipientList>();

            var client = scenarioContext.Get<IClient>();

            SendRecipientListsResponse response = null;
            Task.Run(async () => { response = await client.RecipientLists.Create(recipientList); }).Wait();

            scenarioContext.Set(response);
            scenarioContext.Set<Response>(response);
        }

        [When(@"I retrieve the ""(.*)"" recipient list")]
        public void WhenIRetrieveTheRecipientList(string key)
        {
            var client = scenarioContext.Get<IClient>();

            RetrieveRecipientListsResponse response = null;

            Task.Run(async () =>
            {
                response = await client.RecipientLists.Retrieve(key);
            }).Wait();

            scenarioContext.Set(response.RecipientList);
        }

        [When(@"I update the recipient list")]
        public void WhenIUpdateTheRecipientList()
        {
            var recipientList = scenarioContext.Get<RecipientList>();

            var client = scenarioContext.Get<IClient>();

            UpdateRecipientListResponse response = null;

            Task.Run(async () =>
            {
                response = await client.RecipientLists.Update(recipientList);
            }).Wait();

            scenarioContext.Set(response);
        }

        [Then(@"it should have the following recipient list values")]
        public void ThenItShouldHaveTheFollowingRecipientListValues(Table table)
        {
            var recipientList = scenarioContext.Get<RecipientList>();
            table.CompareToInstance(recipientList);
        }

        [Then(@"it should have the following recipients")]
        public void ThenItShouldHaveTheFollowingRecipients(Table table)
        {
            var recipientLists = scenarioContext.Get<RecipientList>()
                .Recipients
                .Select(x => new {x.Address.Email});
            table.CompareToSet(recipientLists);
        }
    }
}