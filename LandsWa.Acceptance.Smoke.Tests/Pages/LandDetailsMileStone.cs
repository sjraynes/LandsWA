using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    internal class LandDetailsMileStone : BasePage
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

        protected string LGAName = null;

        public LandDetailsMileStone(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            GetElementByXpath(staticPageElement).Click();
        }

        public LandDetailsMileStone AddLandRecordForLGA(string LGAName)
        {
            ClickOnAddLandRecordImage();
            AddLGA(LGAName);
            ClickAddButton();
            return this;
        }

        public LandDetailsMileStone ClickOnAddLandRecordImage()
        {
            GetElementByXpath(addLandRecordImg).Click();
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

        public ConsultationMileStone ClickContinueButton()
        {
            GetElementByXpath(continueButton).Click();
            return new ConsultationMileStone(_driver);
        }
    }
}
