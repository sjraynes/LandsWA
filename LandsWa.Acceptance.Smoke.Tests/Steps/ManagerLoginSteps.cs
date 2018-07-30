using LandsWa.Acceptance.Smoke.Tests.Helper;
using LandsWa.Acceptance.Smoke.Tests.Pages;
using LandsWa.Acceptance.Smoke.Tests.SetupTeardown;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;

namespace LandsWa.Acceptance.Smoke.Tests
{
    [Binding]
    public class ManagerLoginSteps : BaseTestFeature
    {
        protected TeamDashboardPage teamDashboard;
        protected LoginPage loginPage;
        protected CommonSteps com;

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            loginPage = (new CommonSteps(Driver))
                .BrowseToLoginPage();
        }
        
        [Given(@"I enter Username and password")]
        public void GivenIEnterUsernameAndPassword(Table table)
        {
            var credentials = table.CreateInstance<Constants>();
            loginPage.EnterUsername(credentials.AssManagerUsername, User.Manager);
            loginPage.EnterPassword(credentials.AssManagerPassword);
        }

        [When(@"I click on Login button")]
        public void WhenIClickOnLoginButton()
        {
            switch(loginPage.userProfile)
            {
                case User.Manager:
                    teamDashboard = (TeamDashboardPage)loginPage.ClickLoginButton();
                    break;
            }
        }
        
        [Then(@"I should be taken to team dashboard")]
        public void ThenIShouldBeTakenToTeamDashboard()
        {
            Assert.IsTrue(teamDashboard.IsPageLoadComplete());
        }

        [Then(@"Manager name '(.*)' should be displayed on the Team Dashboard")]
        public void ThenManagerNameShouldBeDisplayedOnTheTeamDashboard(string name)
        {
            Assert.IsTrue(teamDashboard.IsManagerNameDisplayed(name));
        }

    }
}
