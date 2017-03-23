using TechTalk.SpecFlow;
using WatiN.Core;

namespace IntegrationTestingExample.SpecFlowSpec.Infrastructure
{
    [Binding]
    public class WebBrowser
    {
        public const string RootUrl = "http://localhost:8080";

        public static Browser Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                    ScenarioContext.Current["browser"] = new IE();
                return (Browser)ScenarioContext.Current["browser"];
            }
        }

        [AfterScenario]
        public static void Close()
        {
            if (ScenarioContext.Current.ContainsKey("browser"))
                WebBrowser.Current.Close();
        }
    }
}


