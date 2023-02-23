namespace CalculatorTests.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public CalculatorStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            _scenarioContext["first"] = number;
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            _scenarioContext["second"] = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            int first = (int)_scenarioContext["first"];
            int second = (int)_scenarioContext["second"];

            string functionResponse = MakeRequest(first, second);
            _scenarioContext["response"] = functionResponse;
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string result)
        {
            string response = (string)_scenarioContext["response"];
            response.Should().Be(result);
        }

        private string MakeRequest(int first, int second)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"[FUNCTION APP URL]/api/Add?first={first}&second={second}&code=[API KEY]";

                HttpResponseMessage response = client.GetAsync(url).Result;

                string content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}