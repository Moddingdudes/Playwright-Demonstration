using PlaywrightTests.Common;

namespace PlaywrightTests.Home
{
    /// <summary>
    /// Tests the navbar for the home page
    /// </summary>
    public class HomeNavbarTest : NavbarTest
    {
        protected override string PagePathURL => CommonUtils.HomePagePathURL;
    }
}
