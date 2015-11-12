using DataTransfer;

namespace Logic
{
    class TestLogic : ITestLogic
    {
        public SimpleMessage SayHello()
        {
            return new SimpleMessage
            {
                Message = "Hello World"
            };
        }
    }
}
