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
        protected LoginPage loginPage;
        protected TeamDashboardPage teamDashboard;
        dynamic page;

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            loginPage = new LoginPage(Driver);
        }
        
        [Given(@"I enter Username and password for a manager")]
        public void GivenIEnterUsernameAndPasswordForAManager(Table table)
        {
            var credentials = table.CreateInstance<Constants>();
            loginPage.EnterUsername(credentials.ManagerUsername, User.Manager);
            loginPage.EnterPassword(credentials.ManagerPassword);

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
