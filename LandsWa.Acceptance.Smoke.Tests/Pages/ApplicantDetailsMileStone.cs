using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    internal class ApplicantDetailsMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Applicant Details']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2/a[contains(text(), 'Applicant Details')]";
        protected string continueButton = "//button[text()='Continue']";

        public ApplicantDetailsMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement);
        }

        public RequestDetailsMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new RequestDetailsMileStone(_driver);
        }
    }
}
