using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;

namespace Service.Console
{
    class ConsoleIocModule : Module
    {
        LoggerConfiguration LoggerConfiguration { get; }

        public ConsoleIocModule(LoggerConfiguration loggerConfiguration)
        {
            LoggerConfiguration = loggerConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Register types for console application here
            base.Load(builder);
        }
    }
}
