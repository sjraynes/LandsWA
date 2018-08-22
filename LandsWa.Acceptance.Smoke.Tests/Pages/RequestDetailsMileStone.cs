using OpenQA.Selenium;
using System;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class RequestDetailsMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Request Details']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2[contains(text(), 'Request Details')]";
        protected string continueButton = "//button[text()='Continue']";
        protected string genrealRequestType = "//label[text()='General Crown land request']";
        protected string LgaType = "//label[text()='Crown land request from Local Government, Management Body or State Government Agency']";
        protected string EventType = "//label[text()='Crown land request from Local Government, Management Body or State Government Agency']";
        protected string crownLandRequestType = "//label[text()='Crown land request from Local Government, Management Body or State Government Agency']";
        protected string eventRequestRequestType = "//label[text()='Request for access to Crown land for an event']";
        protected string categoryDropdown = "//div[text()='Select one item...']";
        protected string descriptionTextArea = "//textarea";
        protected string CLEFRequestCheckbox = "//label[text()=' Request received on a CLEF']";
        protected string signedCheckbox = "//label[text()='Signed by Applicant']";
        protected string dateReceived = "//label[text()='Date Received']/../../div[2]//input";
        protected string dateSigned = "//label[text()='Date Signed']/../../div[2]//input";
        protected string uploadButton = "//button[text()='Upload']";
        protected string currentDate = DateTime.Now.ToString("dd/MM/yyyy");

        public RequestDetailsMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement).Click();
        }

        public RequestDetailsMileStone SelectGeneralRequestType()
        {
            GetElementByXpath(genrealRequestType).Click();
            return this;
        }

        public RequestDetailsMileStone SelectRequestType(RequestType type)
        {
            switch (type)
            {
                case RequestType.General:
                    GetElementByXpath(genrealRequestType).Click();
                    break;
                case RequestType.LGA:
                    GetElementByXpath(LgaType).Click();
                    break;
                case RequestType.Event:
                    GetElementByXpath(EventType).Click();
                    break;
            }           
            return this;
        }

        public RequestDetailsMileStone SelectCategoryFromDropdown(string name)
        {
            GetElementByXpath(categoryDropdown).Click();
            GetElementByXpath($"//*[text()='{name}']").Click();
            return this;
        }

        public RequestDetailsMileStone EnterDescription(string desc)
        {
            GetElementByXpath(descriptionTextArea).SendKeys(desc);
            return this;
        }

        public RequestDetailsMileStone ClickCLEFRequestCheckbox()
        {
            GetElementByXpath(CLEFRequestCheckbox).Click();
            return this;
        }

        public RequestDetailsMileStone ClickApplicantSignedCheckbox()
        {
            GetElementByXpath(signedCheckbox).Click();
            return this;
        }

        public RequestDetailsMileStone EnterDateSigned()
        {
            GetElementByXpath(dateSigned).SendKeys(currentDate);
            return this;
        }

        public RequestDetailsMileStone EnterDateReceived()
        {
            GetElementByXpath(dateReceived).SendKeys(currentDate);
            return this;
        }

        public RequestDetailsMileStone UploadDocument(string fileName)
        {
            GetElementByXpath(staticPageElement).Click();
            UploadDocument(GetElementByXpath(uploadButton), fileName);
            return this;
        }

        public LandDetailsMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new LandDetailsMileStone(_driver);
        }
    }
}