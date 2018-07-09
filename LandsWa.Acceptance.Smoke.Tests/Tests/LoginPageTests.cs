using LandsWa.Acceptance.Smoke.Tests.Pages;
using LandsWa.Acceptance.Smoke.Tests.SetupTeardown;
using NUnit.Framework;

namespace LandsWa.Acceptance.Smoke.Tests.Tests
{
    public class LoginPageTests : BaseTest
    {
        [Test]
        public void VerifyThatManagerCanLogin()
        {
            LoginPage.EnterUsername("SophiaAss", Helper.Enumerations.User.Manager)
                .EnterPassword("infy4321")
                .ClickLoginButton();
        }
    }
}
