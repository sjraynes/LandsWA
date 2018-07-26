using LandsWa.Acceptance.Smoke.Tests.Pages;
using LandsWa.Acceptance.Smoke.Tests.SetupTeardown;
using NUnit.Framework;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;

namespace LandsWa.Acceptance.Smoke.Tests.Tests
{
    public class LoginPageTests : BaseTest
    {
        TeamDashboardPage teamDashboard;
        LoginPage loginPage;
        MyDashboardPage myDashboard;

        [TestCase("SophiaAss", "Sophia", "infy4321", User.Manager)]
        [TestCase("BenAss", "Ben", "infy4321", User.Officer)]
        public void VerifyThatUserCanLogin(string login, string name, string password, User user)
        {
            loginPage = new LoginPage(Driver);

            var dashboard = loginPage.EnterUsername(login, user)
                .EnterPassword(password)
                .ClickLoginButton();

            switch (loginPage.userProfile)
            {
                case User.Manager:
                    teamDashboard = (TeamDashboardPage)dashboard;
                    Assert.IsTrue(teamDashboard.IsPageLoadComplete());
                    Assert.IsTrue(teamDashboard.IsManagerNameDisplayed(name));
                    break;
                case User.Officer:
                    myDashboard = (MyDashboardPage)dashboard;
                    Assert.IsTrue(myDashboard.IsPageLoadComplete());
                    Assert.IsTrue(myDashboard.IsOfficerNameDisplayed(name));
                    break;
            }
        }
    }
}
