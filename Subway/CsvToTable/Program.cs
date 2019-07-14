using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace CsvToTable
{
    class Program
    {
        #region csv 파일을 datatable 형태로 변환
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
        #endregion

        #region 0-1,1-2,...따위의 컬럼들을 '날짜' 컬럼으로 수직화
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
                for (int i = 4; i < rawDataTable.Columns.Count; ++i)
                    dt.Rows.Add(new object[] { row[0] + " " + rawDataTable.Columns[i].ColumnName.Substring(0, 2) + ":00:00", row[1], row[2], row[3], row[i] });
            return dt;
        }
        #endregion

        #region 0-1,1-2,...따위의 컬럼들을 '날짜' 컬럼으로 수직화, 요일 추가
        private static DataTable ModiFyTable2(DataTable rawDataTable)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.TableName = "Daejeon";

            //기존 열 4개
            for (int i = 0; i < 4; ++i)
                dt.Columns.Add(rawDataTable.Columns[i].ColumnName);
            dt.Columns[0].ColumnName += "시간";
            //추가 열 2개
            dt.Columns.Add("승객수");
            dt.Columns.Add("요일");


            foreach (DataRow row in rawDataTable.Rows)
            {
                DateTime dateTime;
                DateTime.TryParseExact((string)row[0], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime);

                int dayOfWeek = (int)dateTime.DayOfWeek;//.ToString().ToUpper().Substring(0,3);
                for (int i = 4; i < rawDataTable.Columns.Count; ++i)
                {
                    string datetime = row[0] + " " + rawDataTable.Columns[i].ColumnName.Substring(0, 2) + ":00:00";
                    dt.Rows.Add(new object[] { datetime , row[1], row[2], row[3], row[i], dayOfWeek });
                }
                    
            }
                
            return dt;
        }
        #endregion

        #region datatable을 이용하여 mssql 서버에 테이블 생성
        static void ExportDataTableToDatabase(DataTable dt)
        {
            SqlConnection con = new SqlConnection("Data Source=.;uid=sa;pwd=a1234a;database=Subway");
            con.Open();

            // 기존 테이블 삭제
            string deleteQuery = "Drop Table " + dt.TableName;
            SqlCommand cmd = new SqlCommand(deleteQuery, con);
            cmd.ExecuteNonQuery();

            string[] type = { "datetime", "int", "nvarchar(50)", "nvarchar(3)", "int", "int" };

            // 새 테이블 생성
            string sql = "Create Table " + dt.TableName + " (";
            for (int i = 0; i < dt.Columns.Count; i++)
                sql += "[" + dt.Columns[i].ColumnName + "] " + type[i] + ",";
            sql += "PRIMARY KEY(" + dt.Columns[0].ColumnName + "," + dt.Columns[1].ColumnName + "," + dt.Columns[3].ColumnName + "))";

            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            using (var adapter = new SqlDataAdapter("SELECT * FROM " + dt.TableName, con))
            using (var builder = new SqlCommandBuilder(adapter))
            {
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.Update(dt);
            }
            con.Close();
        }
        #endregion

        static void Main(string[] args)
        {
            DataTable rawDataTable = ConvertCSVtoDataTable("../../subway.csv");
            //DataTable rawDataTable = ConvertCSVtoDataTable("D:\\subway.csv");
            DataTable modifiedDataTable = ModiFyTable2(rawDataTable);
            ExportDataTableToDatabase(modifiedDataTable);

            return;

            #region 샘플 코드
            /*
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
            */
            #endregion
        }


    }
}
