using System;
using System.Data;
using System.Data.OleDb;

namespace AdvancedFindAutomation.TestData
{
    public class ReadFromExcel : BaseClass
    {
        public String[] ReadExcelData(string oSheetName, int rowid)
        {
            string connectionString = "Provider=Microsoft.JET.OLEDB.4.0;Data Source=Z:\\AdvancedFind\\AdvancedFindAutomation\\AdvancedFindAutomation\\TestData\\TestData.xls;Extended Properties=\"Excel 8.0;HDR=No;\"";

            OleDbConnection connectin = new System.Data.OleDb.OleDbConnection(@connectionString);

            DataTable dt = new DataTable();

            String query = "SELECT * FROM [" + oSheetName + "$]";

            String[] data = null;
            try
            {
                connectin.Open();

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectin);

                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                dt = ds.Tables[0];

                data = new string[ds.Tables[0].Columns.Count];

                for (int i = 0; i <= ds.Tables[0].Columns.Count; i++)
                {
                    data[i] = dt.Rows[rowid - 1][i + 1].ToString();
                }

                connectin.Close();
                return data;
            }
            catch (Exception e)
            {
                connectin.Close();

                return data;
            }
        }
    }
}