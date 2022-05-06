using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BaseStationsLibrary
{
    static class HandoverCheck
    {
        const string apiUrl = "http://localhost:5000/api/basestation/";
        static HttpClient client = new HttpClient();
        
        public static async Task<double> GetHandover(int id)
        {
            var response = await client.GetAsync(new Uri(apiUrl + id));
            return Convert.ToDouble(response.Content.ReadAsStringAsync());
        }
    }
}
