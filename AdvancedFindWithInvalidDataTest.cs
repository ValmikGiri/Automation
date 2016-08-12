using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Gallio.Framework;
using AdvancedFindAutomation.TestData;

namespace AdvancedFindAutomation.PageDirectory.AdvancedFind
{
    [TestFixture]
    [Author("Valmik")]
    [Importance(MbUnit.Framework.Importance.Serious)]

    public class AdvancedFindWithInvalidDataTest : BaseClass
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
        public void TestCaseForAdvanceFindValidDetails()
        {
            ReadFromExcel REx = new ReadFromExcel();
            //Get Data From Excel Sheet For Entering it on Advace find section fields
            string[] arrAdvanceFind = REx.ReadExcelData("AdvancedFind", 4);
            string[] arrBrowserName = REx.ReadExcelData("BrowserName", 3);

            //Launch application with defined browser and URL 
            LaunchApplication(arrBrowserName[0], URl);

            AdvanceSearchFunctions AF = new AdvanceSearchFunctions(driver);

            //Click On Search Button 
            AF.ClickOnSearchButton(driver);

            //Enter Invalid Data on Advanced find Section
            AF.EnterDataOnAdvanceSearchSection(driver, pSearchTerm: arrAdvanceFind[0], pReleventindustry: arrAdvanceFind[1], pResourceType: arrAdvanceFind[2], pYear: arrAdvanceFind[3]);

            //Click On Search Now Button On Advanced find Section 
            AF.ClickOnSearchNowButton(driver);

            //Verify Nothing Found MEssage
            AF.VerifyNothingFoundMessageText(driver, arrAdvanceFind[4]);

            TestLog.End();

            //Quit Driver
            driver.Quit();

        }
    }
}