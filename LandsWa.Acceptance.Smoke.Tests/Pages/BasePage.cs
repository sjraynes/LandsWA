using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Net;
using OpenQA.Selenium.Interactions;
using log4net;
using System.Reflection;
using System.Threading;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;
using OpenQA.Selenium.Support.PageObjects;
using System.Windows.Forms;
using System.IO;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public abstract class BasePage
    {
        public User userProfile { get; set; }

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IWebDriver _driver;

        protected static WebDriverWait Wait = null;       
        protected abstract By IsPageLoadedBy { get; }

        protected BasePage(IWebDriver driver)
        {
            SetWait(driver, 60);
            if (IsDocumentReady())
                PageFactory.InitElements(driver, this);
            else
                throw new Exception("Page not loaded correctly(BasePage-constructor)");
            _driver = driver;
        }

        public bool IsPageLoaded()
        {
            try
            {
                if(IsDocumentReady())
                    Wait.Until(ExpectedConditions.ElementExists(IsPageLoadedBy));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Same as FindElement only returns null when not found instead of an exception.
        /// </summary>
        /// <param name="by">The search string for finding element</param>
        /// <returns>Returns element or null if not found</returns>
        public static IWebElement FindElementSafe(By by)
        {
            try
            {
                return _driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public static void SetWait(IWebDriver driver, int WaitForElementInSeconds)
        {
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WaitForElementInSeconds));
        }

        private bool IsDocumentReady()
        {
            return Wait.Until(driver =>
            {
                bool isDocumentReady = (bool)((IJavaScriptExecutor)driver).
                    ExecuteScript("return document.readyState").
                    ToString().
                    Equals("complete");
                return isDocumentReady;
            });
        }

        private IWebElement GetElement(By by)
        {
            Thread.Sleep(1500);
            bool pageLoad = IsDocumentReady();
            if (pageLoad)
            {
                Wait.Until(ExpectedConditions.ElementExists(by));
                Wait.Until(ExpectedConditions.ElementToBeClickable(_driver.FindElement(by)));
                Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                return Wait.Until<IWebElement>(d => d.FindElement(by));
            }
            else
                return null;
        }

        #region Get element by id, css, xpath. Get the parent of the current element
        protected IWebElement GetElementByText(string text)
        {
            return GetElement(By.XPath($"//*[text()='{text}']"));
        }

        protected IWebElement GetElementById(string id)
        {
            return GetElement(By.Id(id));
        }

        protected IWebElement GetElementBySelector(string selector)
        {
            return GetElement(By.CssSelector(selector));
        }

        protected IWebElement GetElementByXpath(string selector)
        {
            return GetElement(By.XPath(selector));
        }

        protected IList<IWebElement> GetElementsByXPath(string selector)
        {
            IList<IWebElement> ele =
                Wait.Until<IList<IWebElement>>(d => d.FindElements(By.XPath(selector)));
            return ele;
        }
        #endregion


        #region Scroll/Click On Page to different location
        //public static void ClickAtSpecificPointOnWebElement(IWebElement ele, int xOffsetPercentage, int yOffsetPercentage)
        //{
        //    int width = ele.Size.Width;
        //    int height = ele.Size.Height;
        //    Actions act = new Actions(_driver);
        //    act.MoveToElement(ele).MoveByOffset(xOffsetPercentage* height/100, yOffsetPercentage*width/100).Click().Perform();
        //}

        public static void ScrollToView(IWebElement webElement)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(webElement));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;

            jse.ExecuteScript("arguments[0].scrollIntoView()", webElement);
            Actions actions = new Actions(_driver);
        }

        public static void ScrollToTopOfPage()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
            jse.ExecuteScript("window.scrollBy(0,-250)", "");
        }

        public static void ScrollToBottomOfPage()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
            jse.ExecuteScript("window.scrollBy(0,250)", "");
        }
        #endregion

        #region Miscellanous - Stale element, Clear Cookies, Switch Windows
        public static SelectElement GetStaleElement(IWebDriver driver, IWebElement element)
        {
            bool flag = new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(c =>
            {
                try
                {
                    new SelectElement(element);
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            if (flag)
            {
                return new SelectElement(element);
            }
            else
            {
                return null;
            }
        }

        public static void ClearCookies(IWebDriver driver)
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }

        public static void SwitchWindow(IWebDriver driver, string windowHandle)
        {
            driver.SwitchTo().Window(windowHandle);
        }

        public void ReloadPage()
        {
            _driver.Navigate().Refresh();
            if (IsDocumentReady())
                PageFactory.InitElements(_driver, this);
            else
                throw new Exception("Page DOM not ready after Refresh");
        }
        
        public static IList<IWebElement> findAllLinks()
        {
            IList<IWebElement> elementList = new List<IWebElement>();
            elementList = _driver.FindElements(By.TagName("a"));
            IList<IWebElement> finalList = new List<IWebElement>(); ;
            foreach (IWebElement element in elementList)
            {
                string url = element.GetAttribute("href");
                if (url != null && !url.Contains("tel") && !url.Contains("javascript") && !url.Contains("mailto"))
                {
                    finalList.Add(element);
                }
            }
            return finalList;
        }

        public static bool areLinksBroken(IList<IWebElement> list)
        {
            foreach (IWebElement ele in list)
            {
                if (isLinkBroken(ele.GetAttribute("href")))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isLinkBroken(String url)
        {
            bool result = false;
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
                {
                    if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                    {
                        result = false;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        public static void UploadDocument(IWebElement ele, string filename)
        {
            var filepath = GetFolderPathInProjectRoot("Resources") + filename;
            filepath = Path.GetFullPath(@filepath);

            if (File.Exists(filepath))
                ele.SendKeys(filepath);
            else
                throw new Exception("File to upload does not exist");
            Thread.Sleep(500);

            /*
            Use Send Keys - 
            SendKeys.SendWait(GetFolderPathInProjectRoot("Resources") + filename);

            Use Actions - 
            GetElementByXpath(uploadButton).Click();
            _driver.SwitchTo().ActiveElement().SendKeys(filepath);
            Actions action = new Actions(_driver);
            action.SendKeys("{ENTER}");

            Use AutoIt -
            GetElementByXpath(uploadButton).Click();
            AutoItX.Send(filepath);
            AutoItX.Send("{ENTER}");
            */
        }

        public static string GetFolderPathInProjectRoot(string dirName)
        {
            return Path.Combine(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location), $@"..\..\{dirName}\");
        }

        public static void TakeScreenshot(string saveLocation)
        {
            ITakesScreenshot ssdriver = _driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(saveLocation, ScreenshotImageFormat.Png);
        }
    }
}