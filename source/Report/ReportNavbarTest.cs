using PlaywrightTests.Common;

namespace PlaywrightTests.Report
{
    /// <summary>
    /// Tests the navbar for the home page
    /// </summary>
    public class ReportNavbarTest : NavbarTest
    {
        protected override string PagePathURL => CommonUtils.ReportPagePathURL;
    }
}
