using System;
using System.Diagnostics;
using Autofac;
using CommandLine;
using Logic;
using Serilog;
using Service.CommandLineOptions;
using Service.Console;
using Service.Web;

namespace Service
{
    class Program
    {
        static int Main(string[] args)
        {
            var options = ParseCommandLine(args);
            if (options == null) return 1;

#if DEBUG
            if (options.Debug)
            {
                Debugger.Launch();
            }
#endif

            if (options.Help)
            {
                System.Console.WriteLine(options.GetUsage());
                Environment.Exit(1);
            }

            using (var container = RegisterApplication(options).Build())
            {
                try
                {
                    var app = container.Resolve<IApplication>();
                    app.Run();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An unexpected error occurred.");
                }
            }
            return 0;
        }

        static IProgramOptions ParseCommandLine(string[] args)
        {
            var options = new CommandLineOptions.CommandLineOptions();
            var parser = new Parser(settings =>
            {
                settings.IgnoreUnknownArguments = true;
                settings.CaseSensitive = false;
            });
            if (!parser.ParseArguments(args, options))
            {
                Environment.Exit(1);
            }
            return options;
        }

        static ContainerBuilder RegisterApplication(IProgramOptions options)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(options).As<IProgramOptions>();

            var logConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole();

            builder.RegisterModule<LogicIocModule>();

            if (options.Interactive)
            {
                builder.RegisterModule(new ConsoleIocModule(logConfig));

                builder.RegisterType<ConsoleApplication>().As<IApplication>().SingleInstance();
            }
            else
            {
                builder.RegisterModule(new WebAppIocModule(logConfig));
                builder.RegisterType<WebApplication>().As<IApplication>().SingleInstance();
            }

            Log.Logger = logConfig.CreateLogger();
            builder.RegisterInstance(Log.Logger).As<ILogger>();

            return builder;
        }
    }
}
