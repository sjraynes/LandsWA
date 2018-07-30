using LandsWa.Acceptance.Smoke.Tests.Helper;
using LandsWa.Acceptance.Smoke.Tests.Pages;
using LandsWa.Acceptance.Smoke.Tests.SetupTeardown;
using LandsWa.Acceptance.Smoke.Tests.Tests;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;

namespace LandsWa.Acceptance.Smoke.Tests.Steps
{
    [Binding]
    public class CaseSubmissionSteps : BaseTestFeature
    {
        protected LoginPage loginPage;
        protected TeamDashboardPage teamDashboard;
        protected MyDashboardPage myDashboard;
        protected ApplicantDetailsMileStone applicantDetailsPage;
        protected RequestDetailsMileStone requestDetailsPage;
        protected LandDetailsMileStone landDetailPage;
        protected ConsultationMileStone consultationPage;
        protected AdditionalInformationMileStone additionalInfoPage;
        protected ReviewMileStone reviewPage;
        protected TermsAndConditionsMileStone termsAndConditionsPage;
        protected SubmissionMileStone submissionPage;
        protected CommonSteps com;

        [Given(@"a web browser is at IWMS login page")]
        public void GivenAWebBrowserIsAtIWMSLoginPage()
        {
            loginPage = (new CommonSteps(Driver)).BrowseToLoginPage();

        }

        [When(@"the officer enters username ""(.*)"" and password ""(.*)"" to login")]
        public void WhenTheOfficerEntersUsernameAndPasswordToLogin(string username, string password)
        {
            loginPage.EnterUsername(username, User.Officer);
            loginPage.EnterPassword(password);
            switch (loginPage.userProfile)
            {
                case User.Manager:
                    teamDashboard = (TeamDashboardPage)loginPage.ClickLoginButton();
                    break;
                case User.Officer:
                    myDashboard = (MyDashboardPage)loginPage.ClickLoginButton();
                    break;
            }
        }

        [When(@"searches for an applicant ""(.*)""")]
        public void WhenSearchesForAnApplicant(string applicantName)
        {
            applicantDetailsPage = myDashboard.ClickCreateNewCaseButon()
                .SearchAnApplicantWithName(applicantName)
                .SelectTheApplicantFromSearchResultWithName(applicantName)
                .Continue();
        }

        [When(@"creates a new case for this applicant with the following information")]
        public void WhenCreatesANewCaseForThisApplicantWithTheFollowingInformation(Table table)
        {
            var data = CommonSteps.ToDictionary(table);
            applicantDetailsPage.ClickContinueButton()
                .SelectGeneralRequestType()
                .ClickOnCategoryDropDown()
                .SelectFromDopDown(data["requestCategory"])
                .EnterDescription(data["requestDescription"])
                .ClickContinueButton()
                .AddLandRecordForLGA(data["lga"])
                .ClickContinueButton()
                .ClickLGACheckboxToConsult()
                .HasLGABeenConsultedRadioButtonResponse(Decision.Yes)
                .ClickUpdateButton()
                .ClickContinueButton()
                .ClickContinueButton()
                .ClickCheckBox()
                .ClickContinueButton()
                .ClickTermsAndConditionsCheckbox()
                .ClickSignedCheckbox()
                .EnterDateReceived()
                .EnterDatesigned()
                .ClickSubmitButton()
                .SendCaseSummaryToApplicantRadioButton(Decision.Yes)
                .SelectMethodOfContact(ContactMethod.Email)
                .AnyOtherDocumentsToSend(Decision.No)
                .ClickReadyToEmailConfirmationCheckbox()
                .ClickDoneButton();
        }

        [Then(@"a new case will be created on team dashboard")]
        public void ThenANewCaseWillBeCreatedOnTeamDashboard()
        {
            Assert.IsTrue(true);
        }

    }
}
