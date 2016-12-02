using System;
using System.Threading.Tasks;
using Should;
using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Binding]
    public class MetricsSteps
    {
        [When(@"I query my deliverability for (.*)")]
        public void WhenIQueryMyDeliverability(string metric)
        {
            var client = ScenarioContext.Current.Get<IClient>();
            Response response = null;
            Task.Run(async () =>
            {
                response = await client.Metrics.GetDeliverability(new
                {
                    from = DateTime.MinValue,
                    metrics = metric
                });
            }).Wait();

            ScenarioContext.Current.Set(response);
        }

        [When(@"I query my bounce reasons")]
        public void y()
        {
            var client = ScenarioContext.Current.Get<IClient>();
            Response response = null;
            Task.Run(async () =>
            {
                response = await client.Metrics.GetBounceReasons(new
                {
                    from = DateTime.MinValue
                });
            }).Wait();

            ScenarioContext.Current.Set(response);
        }

        [Then("it should return some metrics count")]
        public void x()
        {
            var response = ScenarioContext.Current.Get<Response>();
            response.ShouldBeType(typeof(GetMetricsResponse));
        }
    }
}