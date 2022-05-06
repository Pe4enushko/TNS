using System;
using System.Data.SqlClient;
using System.Data;

namespace BSLibOnFramework
{
    static class DBWork
    {
        const string connectionString = "Data Source=localhost;Initial Catalog=Yes;Integrated Security=True";
        static SqlConnection Connection = new SqlConnection(connectionString);
        
        public static DataTable GetSquare(int[] ids)
        {
            string cmd = "SELECT [area] FROM [BaseStation] WHERE [id] IN (";
            for (int i = 0; i < ids.Length - 1; i++)
            {
                cmd += ids[i] + ",";
            }
            cmd += $"{ids[ids.Length-1]})";
            return GetData(cmd);
        }
        public static double GetSquare(int id)
        {
            return (double)GetData("SELECT [area] FROM [BaseStation] WHERE [id] = " + id).Rows[0].ItemArray[3];
        }
        static DataTable GetData(string command)
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = command;
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataTable DT = new DataTable();
            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
            DA.Fill(DT);
            return DT;
        }
    }
}
