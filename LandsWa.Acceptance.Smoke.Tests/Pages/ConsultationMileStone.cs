using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    class ConsultationMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Consultation']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2[contains(text(), 'Consultation Details')]";
        protected string checkbox = "//tbody/tr/td[1]/div";
        protected string LGABeenConsultedRadioButton = "//div[@role='radiogroup']/div[1]";
        protected string updateButton = "//button[text()='UPDATE']";
        protected string continueButton = "//button[text()='Continue']";

        public ConsultationMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement).Click();
        }

        public ConsultationMileStone ClickLGACheckboxToConsult()
        {
            GetElementByXpath(checkbox).Click();
            return this;
        }

        public ConsultationMileStone HasLGABeenConsultedRadioButtonResponse(Decision desc)
        {
            switch(desc)
            {
                case Decision.Yes:
                    GetElementByXpath("//div[@role='radiogroup']/div[1]").Click();
                    break;
                case Decision.No:
                    GetElementByXpath("//div[@role='radiogroup']/div[2]").Click();
                    break;
                case Decision.NotApplicable:
                    GetElementByXpath("//div[@role='radiogroup']/div[3]").Click();
                    break;
            }
            return this;
        }

        public ConsultationMileStone ClickUpdateButton()
        {
            GetElementByXpath(updateButton).Click();
            return this;
        }

        public AdditionalInformationMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new AdditionalInformationMileStone(_driver);
        }
    }
}
