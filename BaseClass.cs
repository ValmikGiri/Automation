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
using System.Data;
using System.Data.SqlClient;

namespace AdvancedFindAutomation
{
    public class BaseClass
    {
        
        public IWebDriver driver;

        //Public string for URl and Webrdriver Path
        //Public string for URl and Webrdriver Path
        public string URl = "http://www.microlise.com";
        public string ChromeDriverPath = @"E:\AdvancedFind\AdvancedFindAutomation\packages\Selenium.WebDriver.ChromeDriver.2.23.0.1\driver";
        public string IEDriverPath = @"E:\AdvancedFind\AdvancedFindAutomation\packages\Selenium.WebDriver.IEDriver.2.53.1.1\driver";
        public static string ScreenShotPath = @"E:\AdvancedFind\ScreenShot";
        public string TestDataFile = @"E:\AdvancedFind\AdvancedFindAutomation\AdvancedFindAutomation\TestData\TestData.xlsx";
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
                if (pBrowserName == "Chrome")
                {
                    //initializing Chrome driver
                    driver = new ChromeDriver(ChromeDriverPath);
                }
                if (pBrowserName == "IE")
                {
                    //initializing Chrome driver
                    driver = new IEDriver(IEDriverPath);
                }

                //Enter url 

                driver.Navigate().GoToUrl(pURL);
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
    
        
        //Class And Methods Foe reading data base values
        
        
             //############################# DataBaseVerification ####################################
        public bool CheckDb = false;
        public string DBInstance = "";
        public static string DBName="";
        public string DBUserName = "";
        public string DBPassword = "";
/// <summary>
    /// Data base Verification
    /// </summary>
    public class DBVerificationFunction :BaseClass
    {
        //String variable to store Value From Database
        public string ActualData;
	/// <summary>
        /// 
        /// </summary>
        /// <param name="DBData"></param>
        public DataSet DBConnection(string pQueryString)
        {
            try
            {
                if (CheckDb)
                {
                    string connectionString =
                    "Data Source='" + DBInstance + "';Initial Catalog='" + DBName + "';"
                    + "Integrated Security=false;User Id='" + DBUserName + "';Password='" + DBPassword + "';";

                    // Create and open the connection in a using block. 
                    using (SqlConnection connection =
                    new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlDataAdapter dadapter = new SqlDataAdapter();
                        dadapter.SelectCommand = new SqlCommand(pQueryString, connection);
                        DataSet dset = new DataSet();
                        dadapter.Fill(dset);
                        return dset;
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                TestLog.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="DBData"></param>
        /// <param name="dset"></param>
        public void VerifyData(IWebDriver driver, string[] DBData, DataSet dset)
        {
            if (CheckDb)
            {
                string pColoumnName, pExpectedValue;
                pColoumnName = DBData[0];
                pExpectedValue = DBData[1];

                try
                {
                    if (dset != null && dset.Tables.Count >= 1 && dset.Tables[0].Rows.Count >= 1)
                    {
                        var ActualResults = dset.Tables[0].Rows[0][pColoumnName];
                        string Act = ActualResults.ToString();                                             
                        
                        //Verify Expected  data on UI with Actual Data From DB
                        VerifyStringResult(driver, pExpectedValue, Act, "Expected and Actual database values of " + pColoumnName + " are same", "Expected and Actual database values of " + pColoumnName + " are not same");                        

                    }
                    else
                    {
                        TestLog.WriteLine("Data not saved in database");
                    }

                }
                catch (Exception ex)
                {
                    TestLog.WriteLine(ex.Message);
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="DBData"></param>
        /// <param name="dset"></param>
        public string GetColumnData(IWebDriver driver, string pColoumnName, DataSet dset)
        {       
           
                try
                {   
                    if (CheckDb)
                  {
                    if (dset != null && dset.Tables.Count >= 1 && dset.Tables[0].Rows.Count >= 1)
                    {
                        var ActualResults = dset.Tables[0].Rows[0][pColoumnName];
                        string Act = ActualResults.ToString();
                        return Act;
                    }
                  
                }
                      return null;
                }
                
                catch (Exception ex)
                {
                    TestLog.WriteLine(ex.Message);
                    return null;
                }
             
            }
			}
			}
        

    
}
