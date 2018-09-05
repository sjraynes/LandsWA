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
    class SubmissionMileStoneTest : BaseTest
    {
        LoginPage loginPage;
        MyDashboardPage myDashboard;
        [TestCase("WilmaFlin", "Wilma", "infy4321", User.Officer, "Andrew", "Robert", "Easement", "Joondalup")]
        public void VerifyOfficerIsAbleToPerformNewCaseSubmission(
            string login,
            string name,
            string password,
            User user,
            string applicantName,
            string customerName,
            string category,
            string LGAName
            )
        {
            loginPage = new LoginPage(Driver);

            var dashboard = loginPage.EnterUsername(login, user)
                .EnterPassword(password)
                .ClickLoginButton();

            myDashboard = (MyDashboardPage)dashboard;
            Assert.IsTrue(myDashboard.IsPageLoadComplete());
            Assert.IsTrue(myDashboard.IsOfficerNameDisplayed(name));

            myDashboard.ClickCreateNewCaseButton()
                .SearchAnApplicantWithName(applicantName)
                .SelectTheApplicantFromSearchResultWithName(applicantName)
                .Continue()
                .ClickContinueButton()
                .SelectGeneralRequestType()
                .SelectCategoryFromDropdown(category)
                .EnterDescription("Normal Case created using an Automation script written by the LandsWA Test Team.")
                .ClickCLEFRequestCheckbox()
                .ClickApplicantSignedCheckbox()
                .EnterDateSigned()
                .EnterDateReceived()
                .UploadDocument("Document_1.txt")
                .ClickContinueButton()
                .AddLandRecordForLGA(LGAName)
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
