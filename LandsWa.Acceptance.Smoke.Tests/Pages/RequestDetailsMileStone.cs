using OpenQA.Selenium;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    internal class RequestDetailsMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Request Details']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2[contains(text(), 'Request Details')]";
        protected string continueButton = "//button[text()='Continue']";
        protected string genrealRequestType = "//label[text()='General Crown land request']";
        protected string crownLandRequestType = "//label[text()='Crown land request from Local Government, Management Body or State Government Agency']";
        protected string eventRequestRequestType = "//label[text()='Request for access to Crown land for an event']";
        protected string categoryDropdown = "//div[text()='Select one item...']";
        protected string descriptionTextArea = "//textarea";

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

        public RequestDetailsMileStone ClickOnCategoryDropDown()
        {
            GetElementByXpath(categoryDropdown).Click();
            return this;
        }

        public RequestDetailsMileStone SelectFromDopDown(string name)
        {
            GetElementByXpath($"//*[text()='{name}']").Click();
            return this;
        }

        public RequestDetailsMileStone EnterDescription(string desc)
        {
            GetElementByXpath(descriptionTextArea).SendKeys(desc);
            return this;
        }

        public LandDetailsMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new LandDetailsMileStone(_driver);
        }
    }
}