using System.Collections.Generic;
using IntegrationTestingExample.SpecFlowSpec.Infrastructure;
using TechTalk.SpecFlow;
using System;

namespace IntegrationTestingExample.SpecFlowSpec.StepDefinitions
{
    [Binding]
    public class UserRegistration
    {
        private Dictionary<string, string> passwords = new Dictionary<string, string>();

        [Given(@"I have registered as a user called ""(.*)""")]
        public void GivenIHaveRegisteredAsAUserCalled(string userName)
        {
            passwords[userName] = Guid.NewGuid().ToString();
            WebBrowser.Current.GoTo(WebBrowser.RootUrl + "/Account/Register");
            WebBrowser.Current.TextField("UserName").Value = userName;
            WebBrowser.Current.TextField("Email").Value = "test@example.com";
            WebBrowser.Current.TextField("Password").Value = passwords[userName];
            WebBrowser.Current.TextField("ConfirmPassword").Value = passwords[userName];
            WebBrowser.Current.Forms[0].Submit();
        }

        [Given(@"I am logged in as ""(.*)""")]
        public void GivenIAmLoggedInAs(string userName)
        {
            WebBrowser.Current.GoTo(WebBrowser.RootUrl + "/Account/LogOff");
            WebBrowser.Current.GoTo(WebBrowser.RootUrl + "/Account/LogOn");
            WebBrowser.Current.TextField("UserName").Value = userName;
            WebBrowser.Current.TextField("Password").Value = passwords[userName];
            WebBrowser.Current.Forms[0].Submit();
        }
    }
}