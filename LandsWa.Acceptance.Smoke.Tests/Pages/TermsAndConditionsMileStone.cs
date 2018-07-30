using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class TermsAndConditionsMileStone :BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Terms and Conditions']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2[contains(text(), 'Terms and Conditions')]";
        protected string submitButton = "//button[text()='Submit']";
        protected string termsAndConditionsCheckbox = "//label[text()=' Applicant has read and agreed with the above Terms and Conditions *']";
        protected string signedCheckbox = "//label[text()='Signed by Applicant *']";
        protected string dateReceived = "//label[text()='Date Received']/../../div[2]//input";
        protected string dateSigned = "//label[text()='Date Signed']/../../div[2]//input";
        protected string currentDate = DateTime.Now.ToString("dd/MM/yyyy");

        public TermsAndConditionsMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement).Click();
        }

        public TermsAndConditionsMileStone ClickTermsAndConditionsCheckbox()
        {
            GetElementByXpath(termsAndConditionsCheckbox).Click();
            return this;
        }

        public TermsAndConditionsMileStone ClickSignedCheckbox()
        {
            GetElementByXpath(signedCheckbox).Click();
            return this;
        }

        public TermsAndConditionsMileStone EnterDateReceived()
        {
            GetElementByXpath(dateReceived).SendKeys(currentDate);
            return this;
        }

        public TermsAndConditionsMileStone EnterDatesigned()
        {
            GetElementByXpath(dateSigned).SendKeys(currentDate);
            return this;
        }

        public SubmissionMileStone ClickSubmitButton()
        {
            GetElementByXpath(staticPageElement).Click();
            GetElementByXpath(submitButton).Click();
            return new SubmissionMileStone(_driver);
        }
    }
}
