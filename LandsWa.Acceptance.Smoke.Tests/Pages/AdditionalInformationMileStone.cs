using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class AdditionalInformationMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Additional Information']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2[contains(text(), 'Additional Information')]";
        protected string continueButton = "//button[text()='Continue']";

        public AdditionalInformationMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement).Click();
        }

        public ReviewMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new ReviewMileStone(_driver);
            // adding a comment
            

        }
    }
}
