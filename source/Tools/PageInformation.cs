using Microsoft.Playwright;

namespace PlaywrightTests.Tools
{
    /// <summary>
    /// A POM for the information of all pages. The information in this test contains the CSS engine and the name of the website
    /// Because the information of the pages is static across all sites, this class is reusable for all sites, and there is no specific GotoAsync implementation.
    /// </summary>
    public class PageInformation
    {
        private readonly IPage _page;

        private const string TestInformationQuery = "//div[@id='TestInformation']";

        /// <summary>
        /// Creates a new PageInformation instance.
        /// </summary>
        /// <param name="page">A page reference which already has been loaded for the appropriate page to be tested.</param>
        public PageInformation(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Gets the name of the website.
        /// </summary>
        /// <returns>The inner HTML of the website name element.</returns>
        public ILocator WebsiteName => _page.Locator($"{TestInformationQuery}/h1");

        /// <summary>
        /// Gets the CSS engine of the website.
        /// </summary>
        /// <returns>The inner HTML of the engine statement element.</returns>
        public ILocator CSSEngineStatement => _page.Locator($"{TestInformationQuery}/p");
    }
}
