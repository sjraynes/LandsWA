using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class ApplicantDetailsMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Applicant Details']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2/a[contains(text(), 'Applicant Details')]";
        protected string continueButton = "//button[text()='Continue']";
        protected string UploadConsentDocument = "//input[@class='MultipleFileUploadWidget---ui-inaccessible']";

        public ApplicantDetailsMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement);
        }

        public ApplicantDetailsMileStone UploadAConsentDocument()
        {
            UploadDocument(_driver.FindElement(By.XPath(UploadConsentDocument)), "20180615 Additional Information Document-2.docx");
            return new ApplicantDetailsMileStone(_driver);
        }

        public RequestDetailsMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new RequestDetailsMileStone(_driver);
        }

        public bool GetPageHeading(string heading)
        {
            return GetElementByXpath(staticPageElement).Text.Contains(heading);
        }
    }
}
