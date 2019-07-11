using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace CsvToTable
{
    class Program
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            Encoding encode = Encoding.GetEncoding("ks_c_5601-1987"); // 한글깨짐방지
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath, encode))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                    dt.Columns.Add(header);
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                        dr[i] = rows[i];

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        // TODO: 
        private static DataTable ModiFyTable(DataTable rawDataTable)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.TableName = "Daejeon";

            for (int i = 0; i < 4; ++i)
                dt.Columns.Add(rawDataTable.Columns[i].ColumnName);
            rawDataTable.Columns[0].ColumnName += "시간";
            dt.Columns.Add("승객수");

            foreach (DataRow row in rawDataTable.Rows)
            {
                for (int i = 4; i < rawDataTable.Columns.Count; ++i)
                {
                    dt.Rows.Add(new object[] { row[0] + " " + rawDataTable.Columns[i].ColumnName.Substring(0, 2) + ":00:00", row[1], row[2], row[3], row[i] });
                }
            }
            return dt;
        }

        static void ExportDataTableToDatabase(DataTable dt)
        {
            SqlConnection con = new SqlConnection("Data Source=.;uid=sa;pwd=a1234a;database=Subway");
            con.Open();

            string sql = "Create Table " + dt.TableName + " (";

            string[] type = { "datetime", "int", "nvarchar(50)", "nvarchar(3)", "int" };
            for (int i = 0; i < dt.Columns.Count; i++)
                sql += "[" + dt.Columns[i].ColumnName + "] " + type[i] + ",";

            sql += "PRIMARY KEY(" + dt.Columns[0].ColumnName + "," + dt.Columns[1].ColumnName + "," + dt.Columns[3].ColumnName + "))";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            using (var adapter = new SqlDataAdapter("SELECT * FROM " + dt.TableName, con))
            using (var builder = new SqlCommandBuilder(adapter))
            {
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.Update(dt);
            }
            con.Close();
        }

        static void Main(string[] args)
        {
            DataTable rawDataTable = ConvertCSVtoDataTable("D:\\subway.csv");
            DataTable modifiedDataTable = ModiFyTable(rawDataTable);
            ExportDataTableToDatabase(modifiedDataTable);


            return;
            SqlConnection con = new SqlConnection("Data Source=.;uid=sa;pwd=a1234a;database=Subway");
            con.Open();

            string sql = "Create Table Daejeon (";

            string[] type = { "datetime", "int", "nvarchar(50)", "nvarchar(3)" };

            int i = 0;
            while (i < 4)
                sql += "[" + rawDataTable.Columns[i].ColumnName + "] " + type[i++] + ",";
            while (i < rawDataTable.Columns.Count)
                sql += "[" + rawDataTable.Columns[i++].ColumnName + "] " + "int" + ",";
            sql += "PRIMARY KEY(" + rawDataTable.Columns[0].ColumnName + "," + rawDataTable.Columns[1].ColumnName + "," + rawDataTable.Columns[3].ColumnName + "))";
            //foreach (DataColumn column in dt.Columns)
            //    sql += "[" + column.ColumnName + "] " + "nvarchar(50)" + ",";

            //sql = sql.TrimEnd(new char[] { ',' }) + ")";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();

            using (var adapter = new SqlDataAdapter("SELECT * FROM Daejeon", con))
            using (var builder = new SqlCommandBuilder(adapter))
            {
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.Update(rawDataTable);
                // adapter.Update(ds.Tables[0]); (Incase u have a data-set)
            }
            con.Close();
        }


    }
}
