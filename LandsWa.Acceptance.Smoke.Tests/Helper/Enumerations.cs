using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Helper
{
    public class Enumerations
    {
        /// <summary>
        /// All different Browsers supported by the framework
        /// </summary>
        public enum BrowserType
        {
            Chrome,
            FireFox,
            InternetExplorer
        }

        public enum User
        {
            Officer,
            Manager,
            ExecutiveDirector
        }

        public enum Decision
        {
            Yes,
            No,
            NotApplicable
        }

        public enum ContactMethod
        {
            Email,
            PrintAndPost,
            BothEmailAndPrintAndPost
        }
    }
}
