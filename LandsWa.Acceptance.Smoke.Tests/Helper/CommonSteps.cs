using LandsWa.Acceptance.Smoke.Tests.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;

namespace LandsWa.Acceptance.Smoke.Tests.Helper
{
    public class CommonSteps
    {
        IWebDriver _driver;
        protected LoginPage loginPage;

        public CommonSteps(IWebDriver Driver)
        {
            _driver = Driver;
        }

        public LoginPage BrowseToLoginPage()
        {
            return new LoginPage(_driver);
        }

        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }
    }
}
