using System.Linq;
using System.Threading.Tasks;
using Should;
using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Binding]
    public class SuppressionsSteps
    {
        private readonly ScenarioContext scenarioContext;

        public SuppressionsSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"I add '(.*)' to my suppressions list")]
        public void WhenIAddToMySuppressionsList(string email)
        {
            var client = scenarioContext.Get<IClient>();

            UpdateSuppressionResponse response = null;

            Task.Run(async () => { response = await client.Suppressions.CreateOrUpdate(new [] {email}); }).Wait();

            scenarioContext.Set(response);
            scenarioContext.Set<Response>(response);
        }

        [Then(@"'(.*)' should be on my suppressions list")]
        public void ThenShouldBeOnMySuppressionsList(string email)
        {
            var client = scenarioContext.Get<IClient>();

            ListSuppressionResponse response = null;

            Task.Run(async () =>
            {
                response = await client.Suppressions.Retrieve(email);
            }).Wait();

            response.Suppressions.Count().ShouldEqual(1);
        }
    }
}