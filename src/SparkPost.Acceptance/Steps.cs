using System;
using System.Threading.Tasks;
using Should;
using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Binding]
    public class Steps
    {
        [Given(@"my api key is '(.*)'")]
        public void GivenMyApiKeyIs(string apiKey)
        {
            var client = new SparkPost.Client(apiKey);
            ScenarioContext.Current.Set<IClient>(client);
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
        
        [Then(@"it should return a (.*)")]
        public void ThenItShouldReturnA(int statusCode)
        {
            var response = ScenarioContext.Current.Get<Response>();
            response.StatusCode.GetHashCode().ShouldEqual(statusCode);
        }
    }
}
