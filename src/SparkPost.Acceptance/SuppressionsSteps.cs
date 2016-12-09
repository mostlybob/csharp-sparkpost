using System;
using System.Linq;
using System.Threading;
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

        [Given(@"I have a random email address ending in '(.*)'")]
        public void y(string email)
        {
            scenarioContext["randomemail"] = $"{Guid.NewGuid().ToString().Split('-')[0]}{email}";
        }

        [When(@"I add my random email address a to my suppressions list")]
        public void WhenIAddToMySuppressionsList()
        {
            var email = scenarioContext["randomemail"] as string;

            var client = scenarioContext.Get<IClient>();

            UpdateSuppressionResponse response = null;

            Task.Run(async () => { response = await client.Suppressions.CreateOrUpdate(new [] {email}); }).Wait();

            scenarioContext.Set(response);
            scenarioContext.Set<Response>(response);
        }

        [Then(@"my random email address should be on my suppressions list")]
        public void ThenShouldBeOnMySuppressionsList()
        {
            var email = scenarioContext["randomemail"] as string;

            var client = scenarioContext.Get<IClient>();

            ListSuppressionResponse response = null;

            Thread.Sleep(30000);

            Task.Run(async () =>
            {
                response = await client.Suppressions.Retrieve(email);
                response.Suppressions.Count().ShouldBeGreaterThan(0);
            }).Wait();

        }
    }
}