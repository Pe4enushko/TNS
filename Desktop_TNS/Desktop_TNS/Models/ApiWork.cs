using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Desktop_TNS.Models
{
    static class ApiWork
    {
        static string url = "http://192.168.12.2:5005/api/Equipment/state/"; //TODO: Оно всё null выдаёт.
        static HttpClient client = new HttpClient();
        
        public static async Task<bool> GetStatus(string hardwareId)
        {
            UriBuilder builder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["serialNumber"] = hardwareId;
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(new Uri(url + $"?{hardwareId}"));
            }
            catch(Exception exc)
            { 
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(response.Content);
                Debug.WriteLine(response.ReasonPhrase);
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Ошибка проверки");
            }
            return Convert.ToInt32(response.Content.ReadAsStringAsync()) == 1;
        }
    }
}
