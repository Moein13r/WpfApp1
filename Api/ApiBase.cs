using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    internal class ApiBase
    {
        public static async Task<string> Get(string url)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(url);
            result.EnsureSuccessStatusCode();
            var jsonResult = await result.Content.ReadAsStringAsync();
            return jsonResult;
        }
    }
}
