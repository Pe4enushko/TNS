using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace Desktop_TNS.Models
{
    static class ApiWork
    {
        static string url = "http://localhost:62727/api/Equipment/State?serialNumber="; //TODO: Оно всё null выдаёт.
        static HttpClient client = new HttpClient();
        
        public static async Task<bool> GetStatus(string hardwareId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(new Uri(url + hardwareId));
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Ошибка проверки");
            }
            return await response.Content?.ReadAsStringAsync() == "1";
        }
    }
}
