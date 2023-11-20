using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Common;
using PlaywrightTests.Tools;
using System.Text.RegularExpressions;

namespace PlaywrightTests.Login
{
    /// <summary>
    /// Tests the home page for the application.
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class RegisterPage : PageTest
    {
        [SetUp]
        public async Task SetupPages()
        {
            await Page.GotoAsync(CommonUtils.LoginPageURL);
        }

        [Test]
        public async Task ConfirmButtonResponse()
        {
            TestButtonResultSpecifics homePageTestSpecifics = new TestButtonResultSpecifics(Page);

            ILocator responseButton = homePageTestSpecifics.ResponseButton;

            await responseButton.ClickAsync();

            ILocator responseText = homePageTestSpecifics.ResponseText;

            await Expect(responseText).ToBeVisibleAsync();
        }
    }
}