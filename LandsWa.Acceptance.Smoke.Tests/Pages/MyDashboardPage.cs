using OpenQA.Selenium;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    internal class MyDashboardPage : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("");
        private IWebDriver _driver;

        public MyDashboardPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }
    }
}