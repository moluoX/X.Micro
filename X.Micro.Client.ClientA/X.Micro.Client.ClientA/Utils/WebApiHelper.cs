using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace X.Micro.Client.ClientA.Utils
{
    public class WebApiHelper
    {
        public async static Task<string> InvokeApi(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var req = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };
                var res = await client.SendAsync(req);
                var content = await res.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}
