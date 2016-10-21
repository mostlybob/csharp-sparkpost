using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Binding]
    public class ClientSteps
    {
        [Given(@"my api key is '(.*)'")]
        public void GivenMyApiKeyIs(string apiKey)
        {
            var client = new Client(apiKey);
            ScenarioContext.Current.Set<IClient>(client);
        }
    }
}