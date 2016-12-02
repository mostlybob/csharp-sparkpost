using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Binding]
    public class MetricsSteps
    {
        [When(@"I query my deliverability")]
        public void WhenIQueryMyDeliverability()
        {
            var client = ScenarioContext.Current.Get<IClient>();
            Response response = null;
            Task.Run(async () =>
            {
                response = await client.Metrics.GetDeliverability(new
                {
                    from = DateTime.MinValue,
                    metrics = "count_accepted"
                });
            }).Wait();

            ScenarioContext.Current.Set(response);
        }
    }
}