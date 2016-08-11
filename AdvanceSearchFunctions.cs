using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Gallio.Framework;

namespace AdvancedFindAutomation.PageDirectory
{
    public class AdvanceSearchFunctions : BaseClass
    {
        public AdvanceSearchFunctions(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }

        //******* Locators For Search button *******//

        /// <summary>
        /// Locator for search Button
        /// </summary>
        [FindsBy(How = How.Id, Using = @"searchsubmit")]
        //[CacheLookup]
        public IWebElement SearchButton { get; set; }

        /// <summary>
        /// Locator for search Term Text Box
        /// </summary>
        [FindsBy(How = How.Id, Using = @"searchbox")]
        //[CacheLookup]
        public IWebElement SearchTermTextBox { get; set; }

        /// <summary>
        /// Locator for Relevent Industry Drop Down
        /// </summary>
        [FindsBy(How = How.Id, Using = @"cat")]
        //[CacheLookup]
        public IWebElement ReleventIndustryDropDown { get; set; }

        /// <summary>
        /// Locator for Relevent Industry Drop Down
        /// </summary>
        [FindsBy(How = How.Id, Using = @"post_type")]
        //[CacheLookup]
        public IWebElement ResourceTypeDropDown { get; set; }

        /// <summary>
        /// Locator for Relevent Industry Drop Down
        /// </summary>
        [FindsBy(How = How.Id, Using = @"year")]
        //[CacheLookup]
        public IWebElement yearTypeDropDown { get; set; }


        /// <summary>
        /// Locator for search now Button
        /// </summary>
        [FindsBy(How = How.ClassName, Using = @"button grey")]
        //[CacheLookup]
        public IWebElement SearchNowButton { get; set; }

        //Method to search text on search text box
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="ptext"></param>
        public void EnterTextInSearchTextBox(IWebDriver driver, string pSearchTerm)
        {
            try
            {
                if (pSearchTerm != null)
                {
                    SearchTermTextBox.Clear();
                    SearchTermTextBox.SendKeys(pSearchTerm);
                    TestLog.WriteLine("Enter Text to search ");
                }
            }
            catch (Exception e)
            {
                TestLog.WriteLine("Exception Caught in Enter Text to search method", e.ToString());
            }
        }

        //Method to Select Relevent  industry  value from drop down
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pIndustry"></param>
        public void SelectValueInReleventIndustryDropDown(IWebDriver driver, string pIndustry)
        {
            try
            {
                if (pIndustry != null)
                {
                    new SelectElement(ReleventIndustryDropDown).SelectByText(pIndustry);
                    TestLog.WriteLine("Selected relevent industry as [ {0} ]", pIndustry);
                }
            }
            catch (Exception e)
            {
                TestLog.WriteLine("Exception Caught in Select Relevent Industry method", e.ToString());
            }
        }

        //Method to Select Resource Type  value from drop down
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pResourceType"></param>
        public void SelectValueInResourceTypeDropDown(IWebDriver driver, string pResourceType)
        {
            try
            {
                if (pResourceType != null)
                {
                    new SelectElement(ReleventIndustryDropDown).SelectByText(pResourceType);
                    TestLog.WriteLine("Selected Resource Type as [ {0} ]", pResourceType);
                }
            }
            catch (Exception e)
            {
                TestLog.WriteLine("Exception Caught in Select Resource Type method", e.ToString());
            }
        }

        //Method to Select Year  value from drop down
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pYear"></param>
        public void SelectValueInYearDropDown(IWebDriver driver, string pYear)
        {
            try
            {
                if (pYear != null)
                {
                    new SelectElement(ReleventIndustryDropDown).SelectByText(pYear);
                    TestLog.WriteLine("Selected Year as [ {0} ]", pYear);
                }
            }
            catch (Exception e)
            {
                TestLog.WriteLine("Exception Caught in Select Year method", e.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        public void ClickOnSearchNowButton(IWebDriver driver)
        {
             try
            {
                  SearchNowButton.Click();
                 Thread.Sleep(500);
                   TestLog.WriteLine("Clicked On Search now button");
             }
             catch (Exception e)
             {
                 TestLog.WriteLine("Exception Caught in Click Search now button method", e.ToString());
             }
        }

         /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        public void ClickOnSearchButton(IWebDriver driver)
        {
             try
            {
                  SearchButton.Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id(@"searchbox")));
                   TestLog.WriteLine("Clicked On Search  button");
             }
             catch (Exception e)
             {
                 TestLog.WriteLine("Exception Caught in Click Search  button method", e.ToString());
             }
        }

        //Method to perform search operation in one go

        public void EnterDataOnAdvanceSearchSection(IWebDriver driver,string pSearchTerm,string pReleventindustry,string pResourceType,string pYear)
        {
            try
            {
                EnterTextInSearchTextBox(driver,pSearchTerm);
                SelectValueInReleventIndustryDropDown(driver,pReleventindustry);
                SelectValueInResourceTypeDropDown(driver,pResourceType);
                SelectValueInYearDropDown(driver,pYear);
            }
            catch(Exception e)
            {
                TestLog.WriteLine("Exception Caught in Enter Data On Advanced Search method", e.ToString());
            }
        }


    }
}
