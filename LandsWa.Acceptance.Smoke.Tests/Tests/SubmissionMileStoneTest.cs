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
        [TestCase("BenAss", "Ben", "infy4321", User.Officer, "Ravi", "Easement", "Joondalup")]
        public void VerifyThatAssOfficerIsAbleToPerformNewCaseSubmission(
            string login,
            string name,
            string password,
            User user,
            string applicantName,
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

            myDashboard.ClickCreateNewCaseButon()
                .SearchAnApplicantWithName(applicantName)
                .SelectTheApplicantFromSearchResultWithName(applicantName)
                .Continue()
                .ClickContinueButton()
                .SelectGeneralRequestType()
                .SelectCategoryFromDropdown(category)
                .EnterDescription("Descr")
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
