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
            var client = new Client(apiKey);
            ScenarioContext.Current.Set<IClient>(client);
        }

        [Then(@"it should return a (.*)")]
        public void ThenItShouldReturnA(int statusCode)
        {
            var response = ScenarioContext.Current.Get<Response>();
            response.StatusCode.GetHashCode().ShouldEqual(statusCode);
        }
    }
}