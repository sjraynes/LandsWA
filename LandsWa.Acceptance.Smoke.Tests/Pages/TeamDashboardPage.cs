﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class TeamDashboardPage : BasePage
    {
        IWebDriver _driver = null;

        protected override By IsPageLoadedBy => By.XPath("//title[contains(text(),'Team Dashboard - iWMS DoL Officer Site')]");
        //protected IWebElement ManagerName => GetElementByXpath("//span[contains(text(),'Sophia')]");
        
        public TeamDashboardPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public bool IsPageLoadComplete()
        {
            return IsPageLoaded();
        }

        public bool IsManagerNameDisplayed(string name)
        {
            var NameElement = GetElementByXpath($"//strong[contains(text(),'{name}')]");
            return NameElement.Displayed;
        }
    }
}
