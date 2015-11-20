using System.Threading.Tasks;
using System.Web.Http;
using DataTransfer;
using Logic;
using Logic.Api;
using Logic.Constants;

namespace Service.Web.Controllers
{
    public class TestController : ApiController, ITestApi
    {
        ITestLogic Logic { get; }

        public TestController(ITestLogic logic)
        {
            Logic = logic;
        }

        [Route(Constants.TestRoute)]
        [HttpGet]
        public Task<SimpleMessage> Get()
        {
            return Task.FromResult(Logic.SayHello());
        }
    }
}
