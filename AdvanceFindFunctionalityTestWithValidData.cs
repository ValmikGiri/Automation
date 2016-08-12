using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Gallio.Framework;
using AdvancedFindAutomation.TestData;

namespace AdvancedFindAutomation.PageDirectory
{
    [TestFixture]
    [Author("Valmik")]
    [Importance(MbUnit.Framework.Importance.Serious)]

    public class AdvanceFindFunctionalityTestWithValidData : BaseClass
    {
        //Tips And Documentation
        //[TestFixture] - This attribute declares that the class contains some test method
        //[SetUp] - Before running the test, code inside the SetUp is executed
        //[TearDown] - After running the code inside Test, finally code inside TearDown is executed.

        [SetUp]
        public void MySetup()
        {
            SetUpTest();
        }
        [TearDown]
        public void MyTearDown()
        {
            TearDown();
        }

        [Test]
        public void TestCaseForAdvanceFindWithValidDetails()
        {
           ReadFromExcel REx = new ReadFromExcel();
            //Get Data From Excel Sheet For Entering it on Advace find section fields
            string[] arrAdvanceFind = REx.ReadExcelData("AdvancedFind",3);
            //To Launch Application with Chrome 
            string[] arrBrowserName = REx.ReadExcelData("BrowserName", 4);

            //Launch application with defined browser and URL 
            LaunchApplication(arrBrowserName[0], URl);
            
            AdvanceSearchFunctions AF = new AdvanceSearchFunctions(driver);

            //Click On Search Button 
            AF.ClickOnSearchButton(driver);
            
            //Enter Valid Data on Advanced find Section
            AF.EnterDataOnAdvanceSearchSection(driver, pSearchTerm: arrAdvanceFind[0], pReleventindustry: arrAdvanceFind[1], pResourceType: arrAdvanceFind[2], pYear: arrAdvanceFind[3]);

            //Click On Search Now Button On Advanced find Section 
            AF.ClickOnSearchNowButton(driver);

            //Verify header text after searching term
            AF.VerifySearchedTermTextOnHeaderSection(driver, arrAdvanceFind[0]);

            //Verify Searched term text on main page after searching term
            AF.VerifySearchedTermTextOnMainPage(driver, arrAdvanceFind[0]);

            TestLog.End();
           
            //Quit Driver
            driver.Quit();
        }
    }
}
