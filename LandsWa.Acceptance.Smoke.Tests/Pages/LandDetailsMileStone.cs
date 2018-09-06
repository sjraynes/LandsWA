using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class LandDetailsMileStone : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//a[text()='Land Details']/span[text()='Current Step']");
        private IWebDriver _driver;
        protected string staticPageElement = "//h2[contains(text(), 'Land Details')]";
        protected string addLandRecordImg = "//img[@aria-label='Add Land Record']";
        protected string localGovtAuthority = "//span[text()='Local Government Authority']/../..//input";
        protected string selectedLGA = "//span[@class='PickerTokenWidget---label']";
        protected string addButton = "//button[text()='ADD']";
        protected string continueButton = "//button[text()='Continue']";
        protected string selectedLGATableCell = "//tbody/tr/td[9]";
        protected string LandRecordLotNumber = "//label[text()='Lot Number']/../../div[2]/div/input";
        protected string LandRecordPlanNumber = "//label[text()='Plan Number']/../../div[2]/div/input";
        protected string LandRecordPIN = "//label[text()='PIN']/../../div[2]/div/input";
        protected string LandRecordReserveNumberber = "//label[text()='Reserve Number(if applicable)']/../../div[2]/div/input";
        protected string LandRecordStreetNumber = "//label[text()='Street Number']/../../div[2]/div/input";
        protected string LandRecordStreetName = "//label[text()='Street Name']/../../div[2]/div/input";
        protected string LandRecordSuburb = "//span[text()='Suburb']/../..//input";
        protected string selectedLandRecordSuburb = "//span[@class='PickerTokenWidget---label']";

        protected string LandRecordComment = "//label[text()='Land Record Comment']/../../div[2]/div/textarea";

        protected string GenInfoCoOrdinates = "//textarea";

        protected string LGAName = null;
        protected string suburb = null;

        public LandDetailsMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement).Click();
        }

        public LandDetailsMileStone AddLandRecordForLGA(string LGAName, string suburb)
        {
            ClickOnAddLandRecordImage();
            FillLandRecordEntry(suburb);
            AddLGA(LGAName);
            ClickAddButton();
            return this;
        }

        public LandDetailsMileStone AddGeneralInformation()
        {
            AddCoOrdinates();
            return this;
        }

        public LandDetailsMileStone ClickOnAddLandRecordImage()
        {
            GetElementByXpath(addLandRecordImg).Click();
            return this;
        }

        public LandDetailsMileStone FillLandRecordEntry(string fillsuburb)
        {
            GetElementByXpath(LandRecordLotNumber).SendKeys("745301");
            GetElementByXpath(LandRecordPlanNumber).SendKeys("248501");
            GetElementByXpath(LandRecordPIN).SendKeys("8501");
            GetElementByXpath(LandRecordReserveNumberber).SendKeys("6371");
            GetElementByXpath(LandRecordStreetNumber).SendKeys("47");
            GetElementByXpath(LandRecordStreetName).SendKeys("Lambert");

            var ele = GetElementByXpath(LandRecordSuburb);
            ele.SendKeys(fillsuburb);
            Thread.Sleep(1000);
            ele.SendKeys(Keys.ArrowDown);
            Thread.Sleep(1000);
            ele.SendKeys(Keys.Enter);
            this.suburb = GetElementByXpath(selectedLandRecordSuburb).Text;

            GetElementByXpath(LandRecordComment).SendKeys("Land Record created and inserted during an Application Submission. This Comment was added by an Automation Script written by the LandsWA Test Team.");
            return this;
        }

        public LandDetailsMileStone AddLGA(string LGAName)
        {
            var ele = GetElementByXpath(localGovtAuthority);
            ele.SendKeys(LGAName);
            Thread.Sleep(1000);
            ele.SendKeys(Keys.ArrowDown);
            Thread.Sleep(1000);
            ele.SendKeys(Keys.Enter);
            this.LGAName = GetElementByXpath(selectedLGA).Text;
            return this;
        }

        public LandDetailsMileStone ClickAddButton()
        {
            GetElementByXpath(addButton).Click();
            return this;
        }

        public bool IsLandRecordAdded()
        {
            return GetElementByXpath(selectedLGATableCell).Text == LGAName;
        }

        public LandDetailsMileStone AddCoOrdinates()
        {
            GetElementByXpath(GenInfoCoOrdinates).SendKeys("This is the General Information comment that usually holds the LandRecord Co-Ordinates. It is part of the LandDetails page of the Application Support process. Note that this Case and this comment was added using Automated Test Scripts created by the LandsWA Test Team.");
            return this;
        }

        public ConsultationMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new ConsultationMileStone(_driver);
        }
    }
}
