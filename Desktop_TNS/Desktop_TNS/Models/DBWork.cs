using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows;

namespace Desktop_TNS.Models
{
    public static class DBWork
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
        /// <summary>
        /// If status is not closed, then set <paramref name="closed"/> = DateTime.MinValue
        /// </summary>
        /// <param name="id"></param>
        /// <param name="created"></param>
        /// <param name="bill"></param>
        /// <param name="service"></param>
        /// <param name="serviceKind"></param>
        /// <param name="serviceType"></param>
        /// <param name="status"></param>
        /// <param name="hardware"></param>
        /// <param name="problem"></param>
        /// <param name="problemType"></param>
        /// <param name="closed"></param>
        public static void InsertCRMRequest(
            string id, DateTime created, DateTime closed, string bill,
            string service="", string serviceKind="", string serviceType="",
            string status="", string hardware="", string problem="", string problemType="")
        {
            
            string c = "\',\'";
            string values = "'" + id + c + created.ToString("yyyy-MM-dd") + c + bill + c + service + c + serviceKind + c +
                serviceType + c + status + c + hardware + c + problem + "'," + (closed == DateTime.MinValue ? "NULL" : "'" + closed.ToString("yyyy-MM-dd") + "'")
                + ",'" + problemType + "'";
            values = values.Replace("System.Windows.Controls.ComboBoxItem: ", "");
            if (SendData("INSERT INTO [Requests] VALUES (" +
                $"{values})"))
                MessageBox.Show("Заявка успешно отправлена");
            else
                MessageBox.Show("Ошибка");
        }
        /// <summary>
        /// Принимает числа: <br/> 0 (Сетевое оборудование) <br/> 1 (Абонентское оборудование) <br/> 2 (Магистральное оборудование)
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <returns></returns>
        public static DataTable GetHardwareInfo(int tableNumber)
        {
            switch (tableNumber)
            {
                case 0: return GetData("SELECT * FROM [Web_hardware]");
                case 1: return GetData("SELECT * FROM [Subs_hardware]");
                case 2: return GetData("SELECT * FROM [Highway_hardware]");
                default: return null;
            }
        }
        public static bool AddHighWayHardware(string series, string title,double frequency,string fadeCoef,string transferTech, string address)
        {
            series = "'" + series + "',";
            title = "'" + title + "',";
            fadeCoef = ",'" + fadeCoef + "',";
            transferTech = "'" + transferTech + "',";
            address= "'" + address+ "'";
            return SendData("INSERT INTO [Highway_hardware]([series],[title],[frequency],[fade_coefficent],[transmit_technology],[address]) VALUES (" +
                series + title + frequency.ToString().Replace(",",".") + fadeCoef + transferTech + address + ")");
        }
        public static bool AddSubHardware(string series, string title, string ports, string transmit, string speed, string address)
        {
            series = "'" + series + "',";
            title = "'" + title + "',";
            ports = "'" + ports + "',";
            transmit= "'" + transmit + "',";
            speed = "'" + speed + "',";
            address = "'" + address + "'";
            return SendData("INSERT INTO [Subs_hardware]([series],[title],[ports],[transmit_standard],[speed],[address]) VALUES (" +
                series + title + ports + transmit + speed + address + ")");
        }
        public static bool AddWebHardware(string series, string title, string ports_count,
            string transmit, double frequency, string interfaces, string speed, string address)
        {
            series = "'" + series + "',";
            title = "'" + title + "',";
            ports_count = "'" + ports_count + "',";
            transmit = "'" + transmit + "',";
            interfaces = ",'" + interfaces + "',";
            speed = "'" + speed + "',";
            address = "'" + address + "'";
            return SendData("INSERT INTO [Web_hardware]([series],[title],[ports_count],[transmit_standard],[frequency],[interfaces],[speed],[address]) VALUES (" +
                series + title + ports_count + transmit + frequency.ToString().Replace(",", ".") + interfaces + speed + address + ")");
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
        static bool SendData(string command)
        {
            try
            {
                SqlCommand cmd = Connection.CreateCommand();
                cmd.CommandText = command;
                if (Connection.State == ConnectionState.Closed) Connection.Open();
                int rwf = cmd.ExecuteNonQuery();
                if (Connection.State == ConnectionState.Open) Connection.Close();
                
                return rwf > 0;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
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
