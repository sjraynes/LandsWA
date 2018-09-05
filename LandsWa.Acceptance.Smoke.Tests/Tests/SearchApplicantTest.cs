using LandsWa.Acceptance.Smoke.Tests.Pages;
using LandsWa.Acceptance.Smoke.Tests.SetupTeardown;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;

namespace LandsWa.Acceptance.Smoke.Tests.Tests
{
    public class SearchApplicantTest : BaseTest
    {
        LoginPage loginPage;
        MyDashboardPage myDashboard;

        //[TestCase("BenAss", "Ben", "infy4321", User.Officer, "Ravi", "Ganesh")]
        [Category("SteveTest")]
        [TestCase("WilmaFlin", "Wilma", "infy4321", User.Officer, "Andrew", "Robert")]
        public void VerifyThatAnOfficerCanSearchAnApplicantLogin(string login, string name, string password, User user, string applicantName, string customerName)
        {
            loginPage = new LoginPage(Driver);

            var dashboard = loginPage.EnterUsername(login, user)
                .EnterPassword(password)
                .ClickLoginButton();

            myDashboard = (MyDashboardPage)dashboard;
            Assert.IsTrue(myDashboard.IsPageLoadComplete());
            Assert.IsTrue(myDashboard.IsOfficerNameDisplayed(name));

            var applicantDetailsPage = myDashboard.ClickCreateNewCaseButton()
                 .SearchAnApplicantWithName(applicantName)
                 .SelectTheApplicantFromSearchResultWithName(applicantName)
                 .CheckApplicantIsNotCustomer()
                 .SearchACustomerWithName(customerName)
                 .SelectTheApplicantFromSearchResultWithName(customerName)
                 .Continue();

            Assert.IsTrue(applicantDetailsPage.GetPageHeading("Applicant Details"));

            applicantDetailsPage.UploadAConsentDocument()
                .ClickContinueButton()
                .SelectEventRequestType()
                .CompleteEventDetails()
                .ClickContinueButton()
                .AddLandRecordForLGA("Joondalup")
                .ClickContinueButton()
                .ClickLGACheckboxToConsult()
                .HasLGABeenConsultedRadioButtonResponse(Decision.Yes)
                .ClickUpdateButton()
                .ClickContinueButton()
                .ClickContinueButton()
                .ClickCheckBox()
                .ClickContinueButton()
                .SendCaseSummaryToApplicantRadioButton(Decision.Yes)
                .SelectMethodOfContact(ContactMethod.Email)
                .AnyOtherDocumentsToSend(Decision.No)
                .ClickReadyToEmailConfirmationCheckbox()
                .ClickDoneButton();

        }
    }
}
