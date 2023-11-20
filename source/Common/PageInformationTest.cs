using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Tools;
using System.Text.RegularExpressions;

namespace PlaywrightTests.Common
{
    /// <summary>
    /// Tests the common page information, such as the page name and page CSS engine
    /// </summary>
    public abstract class PageInformationTest : PageTest
    {
        protected abstract string PagePathURL { get; }

        public string FullPageURL => CommonUtils.PageURL + PagePathURL;

        private const string TitleExpectation = "Test";

        [SetUp]
        public async Task SetupPages()
        {
            await Page.GotoAsync(FullPageURL);
        }

        /// <summary>
        /// This test will ensure the title element contains a phrase
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ContainsTitleExpectation()
        {
            await Expect(Page).ToHaveTitleAsync(new Regex(TitleExpectation));
        }

        /// <summary>
        /// This test verifies that the application states which CSS engine it is using
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ConfirmPoweredByCSSEngine()
        {
            PageInformation pageInformation = new PageInformation(Page);

            ILocator cssEngineStatementLocator = pageInformation.CSSEngineStatement;

            string statement = await cssEngineStatementLocator.InnerHTMLAsync();

            Assert.IsTrue(statement.Contains(CommonUtils.CSSEngine));
        }
    }
}
