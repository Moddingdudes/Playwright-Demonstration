using PlaywrightTests.Common;

namespace PlaywrightTests.Contact
{
    /// <summary>
    /// Tests the navbar for the home page
    /// </summary>
    public class ContactNavbarTest : NavbarTest
    {
        protected override string PagePathURL => CommonUtils.ContactPagePathURL;
    }
}
