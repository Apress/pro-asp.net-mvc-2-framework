using NUnit.Framework;
using WatiN.Core;

namespace IntegrationTestingExample.WatiNTests
{
    [TestFixture]
    public class UserAccountTests
    {
        private const string rootUrl = "http://localhost:8080";

        [Test]
        public void DisplaysUserNameInPageHeader()
        {
            var userName = "steve2";
            var password = "mysecret";

            using (var browser = CreateBrowser()) {
                // Register a new account
                browser.GoTo(rootUrl + "/Account/Register");
                browser.TextField("UserName").Value = userName;
                browser.TextField("Email").Value = "test@example.com";
                browser.TextField("Password").Value = password;
                browser.TextField("ConfirmPassword").Value = password;
                browser.Forms[0].Submit();
            }
            
            using (var browser = CreateBrowser()) {
                // Log in and check the page caption
                browser.GoTo(rootUrl + "/Account/LogOn");
                browser.TextField("UserName").Value = userName;
                browser.TextField("Password").Value = password;
                browser.Forms[0].Submit();
                browser.GoTo(rootUrl);
                string actualHeaderText = browser.Element("logindisplay").Text;
                StringAssert.Contains("Welcome " + userName + "!", actualHeaderText);
            }
        }

        // Just using IE here, but WatiN can automate Firefox too
        private Browser CreateBrowser() { return new IE(); }
    }
}
