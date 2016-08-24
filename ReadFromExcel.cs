using System;
using System.Data;
using System.Data.OleDb;

namespace AdvancedFindAutomation.TestData
{
    public class ReadFromExcel : BaseClass
    {
          static string strQuery = "";

        public string[] ReadExcelData(string sheetName, int rowId)
        {

            string strExcelFile = @"E:\AdvancedFind\AdvancedFindAutomation\AdvancedFindAutomation\TestData\TestData.xlsx";
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strExcelFile + @";Extended Properties=""Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";

            OleDbDataAdapter Adapter = new OleDbDataAdapter();//Sealed Class- Set of data Command that are used to fill dataset & update data source during db connection.
            OleDbConnection conn = new OleDbConnection(connectionString);
            //Connection Db- Sealed class
            conn.Open();

            DataTable ExcelSheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
//Data Tables are responsible to get DB schema table data
            switch (sheetName)
            {
                case "Browser":
                    strQuery = "select * from [" + sheetName + "$A" + rowId + ":B" + rowId + "]";
                    break;
                case "AdvancedFind":
                    strQuery = "select * from [" + sheetName + "$A" + rowId + ":G" + rowId + "]";
                    break;
                    //Select Sheet and its range
            }

            OleDbCommand cmd = new OleDbCommand(strQuery, conn);
            // Represents an SQL statement or stored procedure to execute against a data source.
            Adapter.SelectCommand = cmd;
            DataSet dsExcel = new DataSet(); //Represents an in-memory cache of data.

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
