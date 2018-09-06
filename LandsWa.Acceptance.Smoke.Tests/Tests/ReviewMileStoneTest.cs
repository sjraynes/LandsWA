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
    public class ReviewMileStoneTest : BaseTest
    {
        LoginPage loginPage;
        MyDashboardPage myDashboard;
        [TestCase("BenAss", "Ben", "infy4321", User.Officer, "Ravi", "Easement", "Joondalup","Ballajura")]
        public void VerifyThatAssOfficerCanReviewCaseInfo(
            string login,
            string name,
            string password,
            User user,
            string applicantName,
            string category,
            string LGAName,
            string suburb
            )
        {
            loginPage = new LoginPage(Driver);

            var dashboard = loginPage.EnterUsername(login, user)
                .EnterPassword(password)
                .ClickLoginButton();

            myDashboard = (MyDashboardPage)dashboard;
            Assert.IsTrue(myDashboard.IsPageLoadComplete());
            Assert.IsTrue(myDashboard.IsOfficerNameDisplayed(name));

            bool termsAndConditionsPageLoaded = myDashboard.ClickCreateNewCaseButton()
                .SearchAnApplicantWithName(applicantName)
                .SelectTheApplicantFromSearchResultWithName(applicantName)
                .Continue()
                .ClickContinueButton()
                .SelectGeneralRequestType()
                .SelectCategoryFromDropdown(category)
                .EnterDescription("Case created for testing the Review page")
                .ClickCLEFRequestCheckbox()
                .ClickApplicantSignedCheckbox()
                .EnterDateSigned()
                .EnterDateReceived()
                .UploadDocument("Document_1.txt")
                .ClickContinueButton()
                .AddLandRecordForLGA(LGAName,suburb)
                .ClickContinueButton()
                .ClickLGACheckboxToConsult()
                .HasLGABeenConsultedRadioButtonResponse(Decision.Yes)
                .ClickUpdateButton()
                .ClickContinueButton()
                .ClickContinueButton()
                .ClickCheckBox()
                .ClickContinueButton()
                .IsPageLoaded();

            Assert.IsTrue(termsAndConditionsPageLoaded, "Terms and conditions page is not loaded.");
        }
    }
}
