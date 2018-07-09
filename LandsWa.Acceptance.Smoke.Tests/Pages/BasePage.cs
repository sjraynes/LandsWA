using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Net;
using OpenQA.Selenium.Interactions;
using log4net;
using System.Reflection;
using System.Threading;
using static LandsWa.Acceptance.Smoke.Tests.Helper.Enumerations;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    public abstract class BasePage
    {
        public User userProfile { get; set; }

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IWebDriver _driver;

        protected static WebDriverWait Wait = null;

        protected static int WaitForThisElement { get; set; } = 1500;

        private const int _seconds = 120;

        protected abstract By IsPageLoadedBy { get; }

        protected BasePage(IWebDriver driver)
        {
            SetWait(driver, _seconds);
            WaitForPageToBeReady(Wait);

            PageFactory.InitElements(driver, this);
            _driver = driver;
        }

        public bool IsPageLoaded()
        {
            try
            {
                _driver.FindElement(IsPageLoadedBy);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsPageLoaded(String xpath)
        {
            try
            {
                _driver.FindElement(By.XPath(xpath));
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

        /// <summary>
        /// Requires finding element by FindElementSafe(By).
        /// Returns T/F depending on if element is defined or null.
        /// </summary>
        /// <param name="element">Current element</param>
        /// <returns>Returns T/F depending on if element is defined or null.</returns>
        public static bool Exists(IWebElement element)
        {
            if (element == null)
            { return false; }
            return true;
        }

        public static void SetWait(IWebDriver driver, int WaitForElementInSeconds)
        {
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WaitForElementInSeconds));
        }

        public static string GetSfIdFromUrl(string url)
        {
            string SfIdWithViewText = url.Substring(url.Length - 23, 23); //@"https://kikkacapitalorg2--kikkatest.lightning.force.com/one/one.app#/sObject/a0s0k000000w90jAAA/view";
            string SfId = SfIdWithViewText.Substring(0, 18);
            return SfId;
        }

        #region Get element by id, css, xpath. Get the parent of the current element
        protected IWebElement GetElementById(string id)
        {
            return _driver.FindElement(By.Id(id));
        }

        protected IWebElement GetElementBySelector(string selector)
        {
            return _driver.FindElement(By.CssSelector(selector));
        }

        protected IWebElement GetElementByXpath(string selector)
        {
            return _driver.FindElement(By.XPath(selector));
        }

        protected IList<IWebElement> GetElementsByXPath(string selector)
        {
            return _driver.FindElements(By.XPath(selector));
        }

        protected IWebElement GetElementParent(IWebElement element)
        {
            return element.FindElement(By.XPath("./parent::*"));
        }

        protected static IWebElement GetElementByText(string text)
        {
            return _driver.FindElement(By.XPath($"//*[text()='{text}']"));
        }

        #endregion

        #region Select an option from drop down
        public static void ClickOnDropDownOption(string option, IList<IWebElement> dropdownOptions)
        {
            foreach (IWebElement ele in dropdownOptions)
                if (ele.Text.Equals(option, StringComparison.OrdinalIgnoreCase))
                {
                    ele.Click();
                    break;
                }
        }

        public static void ClickOnDropDownOption(string option, IWebElement ele)
        {
            SelectElement selectTag = new SelectElement(ele);
            selectTag.SelectByValue(option);
        }
        #endregion

        #region Wait for page or element to load and visible and clickable
        private static void WaitForPageToBeReady(WebDriverWait Wait)
        {
            Thread.Sleep(WaitForThisElement);
            Wait.Until(driver =>
            {
                //bool isAjaxFinished = (bool)((IJavaScriptExecutor)driver).
                //    ExecuteScript("return jQuery.active == 0");
                bool isDocumentReady = (bool)((IJavaScriptExecutor)driver).
                    ExecuteScript("return document.readyState").
                    ToString().
                    Equals("complete");
                //bool isLoaderHidden = (bool)((IJavaScriptExecutor)driver).
                //    ExecuteScript("return $('.overlay').is(':visible') == false");
                return isDocumentReady;
            });
        }

        public static void WaitForElementToExist(By by)
        {
            WaitForPageToBeReady(Wait);
            Wait.Until(ExpectedConditions.ElementExists(by));
        }

        public static void WaitForElementToExist(IWebElement ele)
        {
            WaitForPageToBeReady(Wait);
            Wait.Until(ExpectedConditions.ElementToBeClickable(ele));
        }

        public static void WaitForElementToBeInvisible(By by)
        {
            WaitForPageToBeReady(Wait);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public static void WaitForElementToBeVisible(By by)
        {
            WaitForPageToBeReady(Wait);
            Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
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

        #region Execute JavaScript
        public static string ExecuteJavascript(IWebDriver driver, string javaScript)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var result = js.ExecuteScript(javaScript);
            if (result != null)
                return result.ToString();
            return null;
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

        public IWebElement GetValidator(string field, string validator)
        {
            return _driver.FindElement(By.XPath($"//small[@data-fv-for='{field}' and @data-fv-validator='{validator}']"));
        }

        public void ReloadPage()
        {
            _driver.Navigate().Refresh();
            WaitForPageToBeReady(Wait);
            PageFactory.InitElements(_driver, this);
        }

        public BasePage EnterText(IWebElement el, string text)
        {
            var dataType = el.GetAttribute("data-type");
            bool isAddress = false;
            if (dataType != null && dataType == "hidden-address")
            {
                isAddress = true;

                el = _driver.FindElement(By.CssSelector($"[name='{el.GetAttribute("name")}_google']"));
            }

            el.Clear();

            if (text == "")
            {
                el.SendKeys("randomstring");
                for (int i = 25; i > 0; i--) el.SendKeys(Keys.Backspace);
                return this;
            }

            el.SendKeys(text);

            if (isAddress)
            {
                WaitForElementToExist(By.CssSelector(".pac-container .pac-item"));
                _driver.FindElement(By.CssSelector(".pac-container .pac-item")).Click();
            }

            return this;
        }

        #endregion

        #region Find broken links
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
    }
}