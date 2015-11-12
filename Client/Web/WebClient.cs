using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.Web
{
    class WebClient : IWebClient
    {
        public string ServerUri { get; }

        public WebClient()
        {
            ServerUri = ConfigurationManager.AppSettings["ServerUri"];

        }


        public async Task<string> GetAsString(string endpoint)
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials =  true
            };
            

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(ServerUri);
                client.Timeout = TimeSpan.FromHours(12);
                return await client.GetStringAsync(endpoint);
            }
        }

    }
}
