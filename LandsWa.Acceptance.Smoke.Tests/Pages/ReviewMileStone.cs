using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    internal class ReviewMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Review']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2[contains(text(), 'Review')]";
        protected string checkbox = "//input/..";
        protected string continueButton = "//button[text()='Continue']";

        public ReviewMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement).Click();
        }

        public ReviewMileStone ClickCheckBox()
        {
            GetElementByXpath(checkbox).Click();
            return this;
        }

        public TermsAndConditionsMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new TermsAndConditionsMileStone(_driver);
        }
    }
}
