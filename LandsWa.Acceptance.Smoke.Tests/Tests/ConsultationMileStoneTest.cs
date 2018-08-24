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
    public class ConsultationMileStoneTest : BaseTest
    {
        LoginPage loginPage;
        MyDashboardPage myDashboard;
        [TestCase("BenAss", "Ben", "infy4321", User.Officer, "Ravi", "Easement", "Joondalup")]
        public void VerifyThatAssOfficerCanAddConsultationToCases(
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
                .ClickCLEFRequestCheckbox()
                .ClickApplicantSignedCheckbox()
                .EnterDateSigned()
                .EnterDateReceived()
                .UploadDocument("Document_1.txt")
                .EnterDescription("Descr")
                .ClickContinueButton()
                .AddLandRecordForLGA(LGAName)
                .ClickContinueButton()
                .ClickLGACheckboxToConsult()
                .HasLGABeenConsultedRadioButtonResponse(Decision.Yes)
                .ClickUpdateButton()
                .ClickContinueButton();
        }
    }
}
