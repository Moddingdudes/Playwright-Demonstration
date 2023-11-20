namespace PlaywrightTests.Common
{
    /// <summary>
    /// Common accessories across all tests.
    /// </summary>
    public class CommonUtils
    {
        public const string CSSEngine = "Bulma";
        public const string PageURL = "https://playwright-test-website.saltedpepper21.repl.co";

        public const string HomePagePathURL = "/";
        public const string ContactPagePathURL = "/contact/";
        public const string ReportPagePathURL = "/report/";
        public const string RegisterPagePathURL = "/register/";
        public const string LoginPagePathURL = "/login/";

        public const string HomePageURL = PageURL + HomePagePathURL;
        public const string ContactPageURL = PageURL + ContactPagePathURL;
        public const string ReportPageURL = PageURL + ReportPagePathURL;
        public const string RegisterPageURL = PageURL + RegisterPagePathURL;
        public const string LoginPageURL = PageURL + LoginPagePathURL;
    }
}
