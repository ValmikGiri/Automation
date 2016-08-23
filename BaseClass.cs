using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Html5;
using Gallio.Framework;
using MbUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Drawing.Imaging;

namespace TestApp
{
    public class BaseClass
    {

        public IWebDriver driver;

        //Public string for URl and Webrdriver Path
        //Public string for URl and Webrdriver Path
        public string URl = "http://www.microlise.com";
        public string ChromeDriverPath = @"E:\AutomationFramework\ValmikAutomation\packages\Selenium.WebDriver.ChromeDriver.2.23.0.1\driver";
        public string IEDriverPath = @"E:\AutomationFramework\ValmikAutomation\packages\Selenium.WebDriver.IEDriver.2.53.1.1\driver";
        public static string ScreenShotPath = @"E:\AutomationFramework\ScreenShot";
        public string TestDataFile = @"E:\AutomationFramework\ValmikAutomation\TestData\TestData.xlsx";
        public string BrowserName;
        public StringBuilder verificationErrors;
        public static bool FinalResult = true;

        //Common use methods 
        /// <summary>
        /// Method to launch application with specified URl and Browser
        /// </summary>
        /// <param name="pBrowserName">Test data for passing broser name</param>
        /// <param name="pURL">Test Data for passing URL Name</param>
        public void LaunchApplication(string pBrowserName, string pURL)
        {
            try
            {
                if (pBrowserName =="Chrome")
                {
                    //initializing Chrome driver
                    driver = new ChromeDriver(ChromeDriverPath);
                }
                if (pBrowserName == "IE")
                {
                    //initializing Chrome driver
                    driver = new ChromeDriver(IEDriverPath);
                }

                //Enter url 

                driver.Navigate().GoToUrl(pURL);
                driver.Manage().Window.Maximize();
            }
            catch (Exception e)
            {
                TestLog.WriteLine("Launch Application Method Caught Exception" + e.ToString());
            }
        }

        //Set Up and Tear Down Methods
        /// <summary>
        /// Set Up Method
        /// </summary>
        public void SetUpTest()
        {
            FinalResult = true;
            verificationErrors = new StringBuilder();
        }

        /// <summary>
        /// Tear Down Method
        /// </summary>
        public void TearDown()
        {
            driver.Quit();
        }

        /// <summary>
        /// Method To Verify String Results
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pExpectedResult"></param>
        /// <param name="pActualResult"></param>
        /// <param name="pPassResult"></param>
        /// <param name="pFailResult"></param>
        public void VerifyStringResult(IWebDriver driver, string pExpectedResult, string pActualResult, string pPassResult, string pFailResult)
        {
            try
            {
                if (pExpectedResult == pActualResult)
                {

                    TestLog.WriteLine("Passed: " + pPassResult + " (Expected Value: " + pExpectedResult + "), (Actual Value: " + pActualResult + ")");
                    FinalResult &= true;
                }
                else
                {
                    TestLog.BeginMarker(Gallio.Common.Markup.Marker.DiffChange);
                    TestLog.WriteLine("Failed: " + pFailResult + " (Expected Value: " + pExpectedResult + "), (Actual Value: " + pActualResult + ")");
                    Screenshot screenShot = ((ITakesScreenshot)driver).GetScreenshot();

                    //Generate Random number and append it to the screen shot name to avoid image name duplication
                    Random ran = new Random();
                    int random = ran.Next(1, 1000);
                    string str = RandomString(4, true);
                    screenShot.SaveAsFile(ScreenShotPath + "VerifyStringResult" + random + str + ".png", ImageFormat.Png);
                    TestLog.EmbedImage("VerifyStringResult" + random + str, System.Drawing.Image.FromFile(ScreenShotPath + "VerifyStringResult" + random + str + ".png"));
                    TestLog.End();
                    FinalResult &= false;
                    Assert.Fail();
                }
            }
            catch (Exception e)
            {
                TestLog.WriteLine("Verify String Result Method Caught Exception" + e.ToString());
            }
        }


        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}
