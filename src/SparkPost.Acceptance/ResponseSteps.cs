using Should;
using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Binding]
    public class ResponseSteps
    {
        [Then(@"it should return a (.*)")]
        public void ThenItShouldReturnA(int statusCode)
        {
            var response = ScenarioContext.Current.Get<Response>();
            response.StatusCode.GetHashCode().ShouldEqual(statusCode);
        }
    }
}