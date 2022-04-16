using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Desktop_TNS.Models
{
    static class DBWork
    {
        static SqlConnection Connection = new SqlConnection(Properties.Settings.Default.YesConnectionString);
        /// <summary>
        /// Gets all subscribers with states:
        /// <br>0 = unactive</br>
        /// <br>1 = active</br>
        /// <br>2 = both</br>
        /// </summary>
        /// <param name="state"></param>
        /// <returns>[subscriber_id], [fullname], [contract_id], [bill], [Services]</returns>
        public static DataTable GetAllSubscribersShortData(int state)
        {
            switch (state)
            {
                case 0:
                    return GetData("SELECT [subscriber_id], [fullname], [contract_id], [bill], [Services] " +
                        "FROM [Subscribers] " +
                        "WHERE [termination_date] IS NOT NULL");
                case 1:
                    return GetData("SELECT [subscriber_id], [fullname], [contract_id], [bill], [Services] " +
                        "FROM [Subscribers] " +
                        "WHERE [termination_date] IS NULL");
                default:
                    return GetData("SELECT [subscriber_id], [fullname], [contract_id], [bill], [Services] " +
                        "FROM [Subscribers]");
            }

        }
        /// <summary>
        /// Gets all events for specified role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static List<string> GetEvents(Roles role)
        {
            List<string> result = new List<string>();
            foreach (DataRow item in GetData($"SELECT [event] FROM [Events] WHERE [role_Id] = {(int)role}").Rows)
            {
                result.Add(item.ItemArray[0].ToString());
            }
            return result;
        }
        /// <summary>
        /// Gets all events for specified role number
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static List<string> GetEvents(int roleNumber)
        {
            List<string> result = new List<string>();
            foreach (DataRow item in GetData($"SELECT [event] FROM [Events] WHERE [role_Id] = {roleNumber}").Rows)
            {
                result.Add(item.ItemArray[0].ToString());
            }
            return result;
        }
        public static Roles GetEmployeeRole(string name)
        {
            return (Roles)(int)GetData($"SELECT [position] FROM [Employees] WHERE [name] = '{name}'").Rows[0].ItemArray[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriber_id"></param>
        /// <param name="isTerminated"></param>
        /// <returns>[subscriber_id], [fullname], [passport], [passport_date], [contract_id],[contract_date], [contract_type]
        /// <br>[termination_date],[termination_cause] (if terminated)</br></returns>
        public static DataTable GetOneSubscriberData(string subscriber_id, bool isTerminated)
        {
            return GetData("SELECT [subscriber_Id], [fullname], [passport], [passport_date], [contract_Id]," +
                $" [contract_date], [contract_type]{(isTerminated ? ", [termination_date],[termination_cause]" : "")}" +
                $" FROM [Subscribers]" +
                $" WHERE [subscriber_Id] = '{subscriber_id}'");
        }
        /// <summary>
        /// Gets names and IDs if employees
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmployees()
        {
            return GetData("SELECT [name], [id] FROM [Employees]");
        }
        public static ObservableCollection<string> GetHardwareSeries()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            var DT = GetData("SELECT [series] FROM [Hardware]");
            foreach (DataRow item in DT.Rows)
            {
                result.Add(item.ItemArray[0].ToString());
            }
            return result;
        }
        public static string GetBill(string number = "00000000000")
        {
            return GetData("SELECT [bill] FROM Subscribers WHERE [number] = " + number).Rows[0]?.ItemArray[0].ToString();
        }
        public static ObservableCollection<string> ConvertListToObsCol(List<string> list)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            foreach (var item in list)
            {
                result.Add(item);
            }
            return result;
        }
        public static bool CheckSubscriber(string number, string name)
        {
            if (string.IsNullOrEmpty(number))
            {
                number = "00000000000";
            }
            return GetData($"SELECT * FROM [Subscribers] WHERE [number] = {number} AND [fullname] = '{name}'").Rows.Count > 0;
        }

        #region BasicQueries
        //Queries
        /// <summary>
        /// Use Select command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        static DataTable GetData(string command)
        {
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = command;
            DataTable DT = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            cmd.ExecuteNonQuery();
            DA.Fill(DT);
            if (Connection.State == ConnectionState.Open) Connection.Close();
            return DT;
        }
        /// <summary>
        /// Use Insert command
        /// </summary>
        /// <param name="command"></param>
        static void SendData(string command)
        {
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = command;
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            cmd.ExecuteNonQuery();
            if (Connection.State == ConnectionState.Open) Connection.Close();
        }
        /// <summary>
        /// Checks if requested data exists
        /// </summary>
        /// <param name="Select"></param>
        /// <returns></returns>
        static bool CanSelect(string Select)
        {
            string cmd = "SELECT CASE WHEN EXISTS(" + Select + ") THEN 1 ELSE 0 END";
            DataTable DT = GetData(cmd);
            return (int)DT.Rows[0].ItemArray[0] == 1;
        }
        #endregion
    }
}
