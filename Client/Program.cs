using System;
using System.Threading.Tasks;
using Autofac;
using Client.ServiceClient;
using Client.Web;
using CommandLine;
using Serilog;

namespace Client
{
    class Program
    {
        static int Main(string[] args)
        {
            var options = ParseCommandLine(args);
            if (options == null) return 1;

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var builder = new Autofac.ContainerBuilder();
            builder.RegisterType<WebClient>().As<IWebClient>();
            builder.RegisterInstance(logger).As<ILogger>();
            builder.RegisterInstance(options).As<IProgramOptions>();
            builder.RegisterType<TestClient>().AsSelf();
            builder.RegisterType<SignalRClient>().AsSelf();

            using (var container = builder.Build())
            {
                Run(container).Wait();
            }
            return 0;
        }

        static async Task Run(ILifetimeScope container)
        {
            try
            {
                var signalr = container.Resolve<SignalRClient>();
                await signalr.ConnectSignalR();

                var client = container.Resolve<TestClient>();
                var message = await client.Get();
                Console.WriteLine(message.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("<Enter to quit>");
            Console.ReadLine();
        }

        static IProgramOptions ParseCommandLine(string[] args)
        {
            var options = new CommandLineOptions();
            if (!Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine(options.GetUsage());
                Environment.Exit(1);
            }
            return options;
        }
    }
}
