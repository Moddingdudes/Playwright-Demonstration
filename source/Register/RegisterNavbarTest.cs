using PlaywrightTests.Common;

namespace PlaywrightTests.Register
{
    /// <summary>
    /// Tests the navbar for the home page
    /// </summary>
    public class RegisterNavbarTest : NavbarTest
    {
        protected override string PagePathURL => CommonUtils.RegisterPagePathURL;
    }
}
