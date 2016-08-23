using System;
using System.Data;
using System.Data.OleDb;

namespace TestApp.TestData
{
  
        public class ReadFromExcel : BaseClass
        {
            static string strQuery = "";

            public string[] ReadExcelData(string sheetName, int rowId)
            {
             
                string strExcelFile = @"E:\AutomationFramework\ValmikAutomation\TestApp\TestData\TestData.xlsx";
                var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strExcelFile + @";Extended Properties=""Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";

                OleDbDataAdapter Adapter = new OleDbDataAdapter();
                OleDbConnection conn = new OleDbConnection(connectionString);
                conn.Open();

                DataTable ExcelSheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                switch (sheetName)
                {
                    case "BrowserName":
                        strQuery = "select * from [" + sheetName + "$B" + rowId + ":C" + rowId + "]";
                        break;
                    case "AdvancedFind":
                        strQuery = "select * from [" + sheetName + "$A" + rowId + ":G" + rowId + "]";
                        break;
                }

                OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                Adapter.SelectCommand = cmd;
                DataSet dsExcel = new DataSet();

                Adapter.Fill(dsExcel);                

                cmd = null;
                conn.Close();

                string[] stringArray = new string[dsExcel.Tables[0].Columns.Count];


                for (int col = 0; col < dsExcel.Tables[0].Columns.Count; ++col)
                {
                    stringArray[col] = dsExcel.Tables[0].Rows[0][col].ToString();
                }

                return stringArray;
            }
        }
    }
