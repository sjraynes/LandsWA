using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    internal class AssignApplicantCustomerPage : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//h1[contains(text(),'Assign Applicant/Customer to the New Case')]");
        private IWebDriver _driver;
        protected string IsCustomerTheApplicantNo = "//input[@value='No']/..";
        ApplicantSearch applicantSearch;
        CustomerSearch customerSearch;

        public AssignApplicantCustomerPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            applicantSearch = new ApplicantSearch(_driver);
            customerSearch = new CustomerSearch(_driver);
        }

        public AssignApplicantCustomerPage SearchAnApplicantWithName(string name)
        {
            applicantSearch.EnterFirstName(name)
                .ClickApplyButton();
            return this;
        }

        public AssignApplicantCustomerPage SearchACustomerWithName(string name)
        {
            customerSearch.FindCustomer(name)
                .ClickApplyButton();
            return this;
        }

        public AssignApplicantCustomerPage SelectTheApplicantFromSearchResultWithName(string name)
        {
            applicantSearch.SelectApplicantFromResults(name);
            return this;
        }
        
        public AssignApplicantCustomerPage SelectTheCustomerFromSearchResultWithName(string name)
        {
            customerSearch.SelectCustomerFromResults(name);
            return this;
        }

        public AssignApplicantCustomerPage SetApplicantIsNotCustomer()
        {
            GetElementByXpath(IsCustomerTheApplicantNo).Click();
            return this;
        }

        public ApplicantDetailsMileStone Continue()
        {
            applicantSearch.ClickContinueButton();
            return new ApplicantDetailsMileStone(_driver);
        }

    }

    internal class ApplicantSearch : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//h2[contains(text(),'Search for Applicant')]");
        private IWebDriver _driver;
        protected string staticPageHeading = "//h2[contains(text(),'Search for Applicant')]";
        protected string firstNameField = "(//*[text()='First Name']/../..//input)[1]";
        protected string disabledApplyButton = "//div[@class='Button---disabled_btn_glass']/following-sibling::button[text()='Apply']";
        protected string enabledApplyButton = "//button[contains(text(),'Apply')]";

        protected string searchResultText = "//h2[text()='Search Results']";
        protected string searchResultFirstColumnHeading = "//div[contains(text(),'First Name')]";

        protected string disabledContinueButton = "//div[@class='Button---disabled_btn_glass']/following-sibling::button[text()='Continue']";
        protected string enabledContinueButton = "//button[contains(text(),'Continue')]";

        public ApplicantSearch(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public ApplicantSearch EnterFirstName(string firstName)
        {
            GetElementByXpath(firstNameField).SendKeys(firstName);
            return this;
        }

        public ApplicantSearch ClickApplyButton()
        {
            GetElementByXpath(staticPageHeading).Click();
            GetElementByXpath(enabledApplyButton).Click();
            return this;
        }

        public bool IsApplicantSearchResultEmpty()
        {
            IWebElement ele;
            try
            {
                ele = GetElementByXpath(searchResultFirstColumnHeading);
            }
            catch(Exception e)
            {
                Console.WriteLine("Search Result is empty " + e.Message);
                return false;
            }
            return true;
        }

        public bool ApplicantPresentInSearchResult(string name)
        {
            IWebElement ele;
            try
            {
                ele = GetElementByXpath($"//p[contains(text(),'{name}')]");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Search Result does not contain the name - {name} " + e.Message);
                return false;
            }
            return true;
        }

        public void SelectApplicantFromResults(string name)
        {
            
            IWebElement ele = null;
            try
            {
                ele = GetElementByXpath($"//p[contains(text(),'{name}')]/../../td[1]/div");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Search Result does not contain the name - {name}");
            }
            ele.Click();
        }

        public void ClickContinueButton()
        {
            GetElementByXpath(staticPageHeading).Click();
            GetElementByXpath(enabledContinueButton).Click();
        }

    }

    internal class CustomerSearch : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//h1[contains(text(),'//h2[contains(text(),'Search for Customer')]");
        private IWebDriver _driver;

        // The Customer
        protected string IsCustomerTheApplicantYes = "//input[@value='Yes']";
        protected string customerFirstNameField = "(//*[text()='First Name']/../..//input)[3]";
        protected string CustomerEnabledApplyButton = "(//button[contains(text(),'Apply')])[2]";
        protected string staticPageHeading = "//h2[contains(text(),'Search for Applicant')]";


        public CustomerSearch(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public CustomerSearch FindCustomer(string customerFirstName)
        {
            GetElementByXpath(customerFirstNameField).SendKeys(customerFirstName);
            return this;
        }

        public void SelectCustomerFromResults(string name)
        {

            IWebElement ele = null;
            try
            {
                ele = GetElementByXpath($"//p[contains(text(),'{name}')]/../../td[1]/div");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Search Result does not contain the name - {name}");
            }
            ele.Click();
        }

        public CustomerSearch ClickApplyButton()
        {
            GetElementByXpath(staticPageHeading).Click();
            GetElementByXpath(CustomerEnabledApplyButton).Click();
            return this;
        }
    }
}
