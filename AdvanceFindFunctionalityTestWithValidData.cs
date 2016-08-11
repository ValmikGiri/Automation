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
        public void TestCaseForAdvanceFindValidDetails()
        {
            ReadFromExcel REx = new ReadFromExcel();

            string[] arrAdvanceFind = REx.ReadExcelData("AdvancedFind", 3);

            LaunchApplication(driver, "Chrome", URl);
            
            AdvanceSearchFunctions AF = new AdvanceSearchFunctions(driver);

            AF.ClickOnSearchButton(driver);
            
            AF.EnterDataOnAdvanceSearchSection(driver, pSearchTerm: arrAdvanceFind[0], pReleventindustry: arrAdvanceFind[1], pResourceType: arrAdvanceFind[2], pYear: arrAdvanceFind[3]);
            
            AF.ClickOnSearchNowButton(driver);


           // AF.EnterText(driver, arrAdvanceFind[0]);
        }
    }
}
