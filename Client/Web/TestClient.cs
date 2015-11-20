using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;
using Logic.Api;
using Newtonsoft.Json;
using Service.Web.Controllers;

namespace Client.Web
{
    class TestClient : ITestApi
    {
        public string ServerUri { get; }

        public TestClient()
        {
            ServerUri = ConfigurationManager.AppSettings["ServerUri"];
        }


        public async Task<SimpleMessage> Get()
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials =  true
            };
            

            using (var client = new HttpClient(handler) {BaseAddress = new Uri(ServerUri)})
            {
                client.Timeout = TimeSpan.FromHours(12);
                var service = Refit.RestService.For<ITestApi>(client);
                return await service.Get();
            }
        }

    }
}
