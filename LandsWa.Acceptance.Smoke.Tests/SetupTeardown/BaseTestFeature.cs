using System.Reflection;
using log4net;
using OpenQA.Selenium;
using System;
using LandsWa.Acceptance.Smoke.Tests.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using System.Security.AccessControl;

namespace LandsWa.Acceptance.Smoke.Tests.SetupTeardown
{
    [Binding]
    public class BaseTestFeature
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected static IWebDriver Driver { get; private set; }

        [BeforeTestRun]
        public static void TestSuiteSetup()
        {
            Driver = GetLocalDriver(BrowserType.Chrome);
            Driver.Manage().Window.Maximize();
        }

        [BeforeFeature]
        public static void SetUp()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }

        [AfterFeature]
        public static void TearDown()
        {
            string path = BasePage.GetFolderPathInProjectRoot("ss");
            string method = String.Join("", Regex.Unescape(TestContext.CurrentContext.Test.Name).Split('\"'));
            path = $@"{path}{method}.png";
            BasePage.TakeScreenshot(path);
        }

        [AfterTestRun]
        public static void TestTeardown()
        {
            try
            {
                CleanUpInstances();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

        private static IWebDriver GetLocalDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    /**
                     * ChromeOptions options = new ChromeOptions();
                     * this path needs to be setup on TeamCity build agent as well - about:version
                     * options.AddArguments(@"user-data-dir=C:\Chromium\Temp\Profile\Default");
                     * driver = new ChromeDriver(options);
                     **/
                    ChromeOptions options = new ChromeOptions();
                    //options.AddArguments(@"user-data-dir=C:\Chromium\Temp\Profile\Default");
                    //Driver = new ChromeDriver(options);
                    Driver = new ChromeDriver();
                    break;
                case BrowserType.FireFox:
                    Driver = new FirefoxDriver();
                    break;
                case BrowserType.InternetExplorer:
                    Driver = new InternetExplorerDriver();
                    break;
            }
            return Driver;
        }

        public static void CleanUpInstances()
        {
            if (Driver != null)
            {
                Driver.Dispose();
                Driver = null;
            }
        }
    }
}
