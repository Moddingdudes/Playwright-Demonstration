using PlaywrightTests.Common;

namespace PlaywrightTests.Login
{
    /// <summary>
    /// Tests the navbar for the home page
    /// </summary>
    public class RegisterNavbarTest : NavbarTest
    {
        protected override string PagePathURL => CommonUtils.RegisterPagePathURL;
    }
}
