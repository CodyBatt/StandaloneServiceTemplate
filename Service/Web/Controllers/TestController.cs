using System.Web.Http;
using Logic;

namespace Service.Web.Controllers
{
    public class TestController : ApiController
    {
        ITestLogic Logic { get; }

        public TestController(ITestLogic logic)
        {
            Logic = logic;
        }

        [Route("test")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(Logic.SayHello());
        }

        [Route("sectest")]
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetSecure()
        {
            var message = Logic.SayHello();
            message.Message = message.Message + " - Authenticated";
            return Ok();
        }
    }
}
