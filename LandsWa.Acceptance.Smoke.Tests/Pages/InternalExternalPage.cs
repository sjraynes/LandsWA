using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    internal class InternalExternalPage : BasePage
    {
        // Radio button External and Internal
        // If internal, then Team Name shown, and 'Is a customer Associated with this case?'
        protected override By IsPageLoadedBy => By.XPath("//h1[contains(text(),'Create New Case')]");
        private IWebDriver _driver;
        protected string ExternalRequestOn = "//input[@value='Externally Requested']";
        protected string InternalRequestOn = "//input[@value='Internally Requested']";
        protected string TeamName = "//strong[text()='Team']/../../../../../../div[2]/div/div[2]/p";
        protected string CaseHasACustomer = "//input[@value='Yes']";
        protected string CaseHasNoCustomer = "//input[@value='No']";
        protected string DoneButton = "//button[text()='Done']";


        public InternalExternalPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public InternalExternalPage SetRequestToExternal()
        {
            //Assert.IsTrue.Equals(TeamName, )
            GetElementByXpath(ExternalRequestOn).Click();
            //applicantSearch.EnterFirstName(name)
            //    .ClickApplyButton();
            return this;
        }

        public InternalExternalPage SetRequestToInternal()
        {
            //Assert.IsTrue.Equals(TeamName, )
            GetElementByXpath(InternalRequestOn).Click();
            //applicantSearch.EnterFirstName(name)
            //    .ClickApplyButton();
            return this;
        }

        public InternalExternalPage InternalCaseHasCustomer()
        {
            //Assert.IsTrue.Equals(TeamName, )
            GetElementByXpath(CaseHasACustomer).Click();
            //applicantSearch.EnterFirstName(name)
            //    .ClickApplyButton();
            return this;
        }

        public InternalExternalPage InternalCaseDoesNotHaveCustomer()
        {
            //Assert.IsTrue.Equals(TeamName, )
            GetElementByXpath(CaseHasNoCustomer).Click();
            //applicantSearch.EnterFirstName(name)
            //    .ClickApplyButton();
            return this;
        }

        public AssignApplicantCustomerPage ClickDoneButton()
        {
            GetElementByXpath(DoneButton).Click();
            return new AssignApplicantCustomerPage(_driver);

        }
    }
}
