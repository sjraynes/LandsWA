using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class SubmissionMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Submission']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2[contains(text(), 'Submission Receipt')]";
        protected string methodOfContact = "//span[text()='Method of contact *']/../../div[2]";
        protected string printAndPostDocumentSentDate = "//input[contains(@class,'DatePicker')]";
        protected string readyToEmailCheckbox = "//label[text()='* Confirm ready to email']";
        protected string doneButton = "//button[text()='DONE']";

        public SubmissionMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement).Click();
        }

        public SubmissionMileStone SendCaseSummaryToApplicantRadioButton(Decision decision)
        {
            switch(decision)
            {
                case Decision.Yes:
                    GetElementByXpath("//label[text()='Yes']").Click();
                    break;
                case Decision.No:
                    GetElementByXpath("//label[text()='No']").Click();
                    break;
            } 
            return this;
        }

        public SubmissionMileStone SelectMethodOfContact(ContactMethod contactMethod)
        {
            GetElementByXpath(methodOfContact).Click();
            switch(contactMethod)
            {
                case ContactMethod.Email:
                    GetElementByXpath("//*[text()='Email']").Click();
                    break;
                case ContactMethod.PrintAndPost:
                    GetElementByXpath("//*[text()='Print & Post']").Click();
                    break;
                case ContactMethod.BothEmailAndPrintAndPost:
                    GetElementByXpath("//*[text()='Both Email and Print & Post']").Click();
                    break;
            }
            return this;
        }
        
        public SubmissionMileStone AnyOtherDocumentsToSend(Decision decision)
        {
            switch(decision)
            {
                case Decision.Yes:
                    GetElementByXpath($"//span[contains(text(),'Are there other documents to send to')]/../../../../div[2]//label[text()='Yes']").Click();
                    break;
                case Decision.No:
                    GetElementByXpath($"//span[contains(text(),'Are there other documents to send to')]/../../../../div[2]//label[text()='No']").Click();
                    break;
            }
            return this;
        }

        public SubmissionMileStone EnterTheDatePrintAndPostDocumentWereSent()
        {
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            GetElementByXpath(printAndPostDocumentSentDate).SendKeys(currentDate);
            return this;
        }

        public SubmissionMileStone ClickReadyToEmailConfirmationCheckbox()
        {
            GetElementByXpath(readyToEmailCheckbox).Click();
            return this;
        }

        public void ClickDoneButton()
        {
            GetElementByXpath(doneButton).Click();
        }
    }
}
