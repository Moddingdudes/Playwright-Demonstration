using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Tools;

namespace PlaywrightTests.Common
{
    /// <summary>
    /// An abstract test for Navbars.
    /// Because the Navbar is the same across all pages, this test can be reused and verify functionality
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public abstract class NavbarTest : PageTest
    {
        protected abstract string PagePathURL { get; }

        public string FullPageURL => CommonUtils.PageURL + PagePathURL;

        [SetUp]
        public async Task SetupPages()
        {
            await Page.GotoAsync(FullPageURL);
        }

        [Test]
        public async Task ConfirmBadgeVisibleAndFunctional()
        {
            NavbarInterface navbarInterface = new NavbarInterface(Page);

            ILocator navbarBadge = navbarInterface.NavbarBadge;

            await Expect(navbarBadge).ToBeVisibleAsync();

            await navbarBadge.ClickAsync();

            //Badge always takes you to home
            await Page.WaitForURLAsync($"**{CommonUtils.HomePagePathURL}");

            //Ensure we are now at the Home URL
            Assert.IsTrue(Page.Url == CommonUtils.HomePageURL);
        }

        [Test]
        public async Task ConfirmHomeVisible()
        {
            NavbarInterface navbarInterface = new NavbarInterface(Page);

            ILocator navbarHome = navbarInterface.NavbarHome;

            await Expect(navbarHome).ToBeVisibleAsync();
        }

        [Test]
        public async Task ConfirmContactVisible()
        {
            NavbarInterface navbarInterface = new NavbarInterface(Page);

            ILocator navbarContact = navbarInterface.NavbarContact;

            await Expect(navbarContact).ToBeVisibleAsync();
        }

        [Test]
        public async Task ConfirmReportAnIssueVisible()
        {
            NavbarInterface navbarInterface = new NavbarInterface(Page);

            ILocator navbarMore = navbarInterface.NavbarMore;

            await navbarMore.HoverAsync();

            ILocator navbarReport = navbarInterface.NavbarReportIssue;

            await Expect(navbarReport).ToBeVisibleAsync();
        }

        [Test]
        public async Task ConfirmSignupVisible()
        {
            NavbarInterface navbarInterface = new NavbarInterface(Page);

            ILocator navbarSignup = navbarInterface.NavbarSignUp;

            await Expect(navbarSignup).ToBeVisibleAsync();
        }

        [Test]
        public async Task ConfirmLoginVisible()
        {
            NavbarInterface navbarInterface = new NavbarInterface(Page);

            ILocator navbarLogin = navbarInterface.NavbarLogin;

            await Expect(navbarLogin).ToBeVisibleAsync();
        }
    }
}
