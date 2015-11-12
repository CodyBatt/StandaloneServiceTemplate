using Logic;

namespace Service.Console
{
    class ConsoleApplication : IApplication
    {
        ITestLogic Logic { get; set; }

        public ConsoleApplication(ITestLogic logic)
        {
            Logic = logic;
        }

        public void Run()
        {
            var message = Logic.SayHello();
            System.Console.WriteLine(message.Message);
        }
    }
}