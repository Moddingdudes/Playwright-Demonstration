using Microsoft.Playwright;
using PlaywrightTests.Common;

namespace PlaywrightTests.Tools
{
    /// <summary>
    /// Interfaces with the Navbar to simplify reusable components of 
    /// </summary>
    public class NavbarInterface
    {
        private readonly IPage _page;

        private const string NavbarElement = "//nav";
        private const string NavbarMobilityItems = $"{NavbarElement}//div[@id='navbarelements']";

        /// <summary>
        /// Creates a new Navbar Interface
        /// </summary>
        /// <param name="page">A page reference which already has been loaded for the appropriate page to be tested.</param>
        public NavbarInterface(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Gets the Navbar badge image locator which is located at the top left of the navbar
        /// </summary>
        public ILocator NavbarBadge => _page.Locator($"{NavbarElement}//a[@id='companylogo']");

        /// <summary>
        /// Gets the Navbar home link locator
        /// </summary>
        public ILocator NavbarHome => _page.Locator($"{NavbarMobilityItems}//a[@href='{CommonUtils.HomePagePathURL}']");

        /// <summary>
        /// Gets the Navbar contact link locator
        /// </summary>
        public ILocator NavbarContact => _page.Locator($"{NavbarMobilityItems}//a[@href='{CommonUtils.ContactPagePathURL}']");

        /// <summary>
        /// Gets the Navbar more section locator
        /// </summary>
        public ILocator NavbarMore => _page.Locator($"//a[text()='More']");

        /// <summary>
        /// Gets the Navbar report an issue link locator
        /// </summary>
        public ILocator NavbarReportIssue => _page.Locator($"{NavbarMobilityItems}//a[@href='{CommonUtils.ReportPagePathURL}']");

        /// <summary>
        /// Gets the Navbar sign up link locator
        /// </summary>
        public ILocator NavbarSignUp => _page.Locator($"{NavbarElement}//a[@href='{CommonUtils.RegisterPagePathURL}']");

        /// <summary>
        /// Gets the Navbar log in link locator
        /// </summary>
        public ILocator NavbarLogin => _page.Locator($"{NavbarElement}//a[@href='{CommonUtils.LoginPagePathURL}']");
    }
}
