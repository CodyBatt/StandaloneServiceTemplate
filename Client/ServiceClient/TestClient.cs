using System.Threading.Tasks;
using Client.Web;
using DataTransfer;
using Newtonsoft.Json;

namespace Client.ServiceClient
{
    class TestClient
    {
        IWebClient WebClient { get; }

        private const string Endpoint = "test";
        private const string AuthenticatedEndpoint = "sectest";

        public TestClient(IWebClient webClient)
        {
            WebClient = webClient;
        }

        public async Task<SimpleMessage> Get()
        {
            var result =  await WebClient.GetAsString(Endpoint);
            return JsonConvert.DeserializeObject<SimpleMessage>(result);
        }

        public async Task<SimpleMessage> GetAuthenticated()
        {
            var result = await WebClient.GetAsString(AuthenticatedEndpoint);
            return JsonConvert.DeserializeObject<SimpleMessage>(result);
        }
    }
}
