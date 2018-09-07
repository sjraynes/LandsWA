using OpenQA.Selenium;
using System;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class MyDashboardPage : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//title[contains(text(),'My Dashboard - iWMS DoL Officer Site')]");
        private IWebDriver _driver;

        public MyDashboardPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public bool IsPageLoadComplete()
        {
            return IsPageLoaded();
        }

        public bool IsOfficerNameDisplayed(string name)
        {
            var NameElement = GetElementByXpath($"//strong[contains(text(),'{name}')]");
            return NameElement.Displayed;
        }

        internal InternalExternalPage ClickCreateNewCaseButton()
        {
            IWebElement ele = null;
            try
            {
                ele = GetElementByXpath("//button[contains(text(),'CREATE NEW CASE')]");
            }
            catch(Exception e)
            {
                Console.WriteLine("'CREATE NEW CASE' button is not available to this user");
                Console.WriteLine(e.InnerException);
            }
            ele.Click();
            return new InternalExternalPage(_driver);
        }
    }
}
