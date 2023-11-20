using Microsoft.Playwright;

namespace PlaywrightTests.Tools
{
    /// <summary>
    /// Tests specific parts of the application for the home page, such as button clicks and response headers.
    /// </summary>
    public class TestButtonResultSpecifics
    {

        private const string ResponseTestsSection = "//div[@id='responsetest']";

        private readonly IPage _page;

        public TestButtonResultSpecifics(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Gets the response button on the home page
        /// </summary>
        public ILocator ResponseButton => _page.Locator($"{ResponseTestsSection}//button[text()='Test Result']");

        /// <summary>
        /// Gets the response text that shows up when the response button is clicked
        /// </summary>
        public ILocator ResponseText => _page.Locator($"{ResponseTestsSection}//h3[@id='responsetesttext']");
    }
}
