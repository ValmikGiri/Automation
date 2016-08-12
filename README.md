# Automation
TestAutomationSelenium
OS- Microsoft Windows 2010

Required Setups

1.Microsoft Visual Studio 2010 or Higher
2.Gallio Test Runner -GallioBundle-3.4.14.0
3.Microsoft Office Excel 2010


Refrences Needs To be added to visual studio

1.Selenium Webdriver
2.Selenium Webdriver Support
3.gallio.dll
4.MbUnit.dll
5.Microsoft.Office.Interop.Excel
6.Microsoft Office 14.0 Object Library

Class Files-
1.BaseClass.cs- Parent Class
2.ReadFromExcel.cs- class for Read Excel data(namespace AdvancedFindAutomation.TestData)
3.AdvanceSearchFunctions.cs- Class For Locators and Methods(namespace AdvancedFindAutomation.PageDirectory.AdvancedFind)
4.AdvancedFindWithInvalidDataTest.cs- Class For Test with Invalid Data (namespace-AdvancedFindAutomation.PageDirectory.AdvancedFind)
5.AdvanceFindFunctionalityTestWithValidData.cs-Class For Test with Valid Data(namespace-AdvancedFindAutomation.PageDirectory.AdvancedFind)




@@@@@Running Test Using MbUnit
Test Runner – Mbunit Framework (Gallio Icarus)
This document deals with 
1.	Download and add references to visual studio to run Test Using Gallio Icarus test runner
2.	How to run test using Gallio Icarus.
Tools required
1. VS2010 or higher
2. Gallio v3.4.11 and Mbunit 3.4.0
Steps to add references 
1.Download the latest Version of Gallio Icarus Test Runner(Zip File)
Url To Download Gallio - https://code.google.com/archive/p/mb-unit/downloads
2.Extraxt the Zip file and keep it on physical path 
3.Open Visual Studio and ->References->Add References and click on add References
4.On Popup select Browse tab->Browse to the Folder (Ex- Drive/ GallioBundle-3.4.14.0/bin)
5.From  Bin Folder select and Add  -> Gallio.dll and MbUnit.dll references.

Running Test Using Gallio Test Runner
Once the references are added one can run test using Gallio test runner with simple steps as follows
Visual Studio Side Changes 
1. Add below two using statements in every test 
Using Gallio. Framework;
Using MbUnit.Framework;
2.Test Attributes needs to be added in every test  i.e. [TestFixture],[SetUp],[TearDown],[Test]
(Need to Write Test Below [Test] attribute.)
3. Build the solution and fix errors if any.->It will create a dll file under bin folder of Project
(Ex- Drive/ProjectSolutionName/bin/debug/ ProjectSolutionName.dll)
Gallio Icarus side changes –
1.Run Gallio.Icarus.exe ( Drive:\Setups\GallioBundle-3.4.14.0\bin\Gallio.Icarus.exe) to open test runner
2.Click On Add File on Gallio Icarus as shown in below screen shot
 
2.It will open PopUp ->navigate to the path (Ex- Drive/ProjectSolutionName/bin/debug/ ProjectSolutionName.dll) where dll is created and open that dll.
3.Which will show all the tests created in project 
4.Select the test and Click on Start Button to run the test. 

