using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;
using LandsWa.Acceptance.Smoke.Tests.Helper;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public class LoginPage : BasePage
    {
        IWebDriver _driver = null;


        protected override By IsPageLoadedBy => By.XPath("//title[contains(text(),'Appian for WA Department of Lands (TEST)')]");
        public IWebElement Username => GetElementByXpath("//input[@id='un']");
        public IWebElement Password => GetElementByXpath("//input[@id='pw']");
        public IWebElement SubmitButton => GetElementByXpath("//input[@type='submit']");

        public LoginPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(Constants.LoginUrl);
        }

        public LoginPage EnterUsername(string username, User user)
        {
            userProfile = user;
            Username.SendKeys(username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            Password.SendKeys(password);
            return this;
        }

        public BasePage ClickLoginButton()
        {
            SubmitButton.Click();
            switch (userProfile)
            {
                case User.Officer:
                    return new MyDashboardPage(_driver);
                case User.Manager:
                    return new TeamDashboardPage(_driver);
                default:
                    break;
            }
            return null;
        }
    }
}
