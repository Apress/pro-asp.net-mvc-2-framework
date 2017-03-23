using IntegrationTestingExample.SpecFlowSpec.Infrastructure;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTestingExample.SpecFlowSpec.StepDefinitions
{
    [Binding]
    public class Homepage
    {
        [When(@"I go to the homepage")]
        public void WhenIGoToTheHomepage()
        {
            WebBrowser.Current.GoTo(WebBrowser.RootUrl);
        }

        [Then(@"the page header should display ""(.*)""")]
        public void ThenThePageHeaderShouldDisplay(string text)
        {
            string actualHeaderText = WebBrowser.Current.Element("logindisplay").Text;
            StringAssert.Contains(text, actualHeaderText);
        }
    }
}