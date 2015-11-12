using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Serilog;
using Service.Signalr;
using Module = Autofac.Module;

namespace Service.Web
{
    class WebAppIocModule : Module
    {
        LoggerConfiguration LoggerConfiguration { get; }
        public WebAppIocModule(LoggerConfiguration loggerConfiguration)
        {
            LoggerConfiguration = loggerConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            LoggerConfiguration.WriteTo.RollingFile(AppDomain.CurrentDomain.BaseDirectory + "\\app-{Date}.log")
                    .WriteTo.SignalR(GlobalHost.ConnectionManager.GetHubContext<StatusMessageHub>());

            builder.RegisterInstance(Startup.ConfigureWebApi()).AsSelf();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<WebApplicationService>().As<IService>().SingleInstance();
        }
    }
}
