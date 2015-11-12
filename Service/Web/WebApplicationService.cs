using System;
using System.Configuration;
using System.Net;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using Serilog;

namespace Service.Web
{
    class WebApplicationService : IService
    {
        
        private readonly HttpConfiguration _config;
        ILifetimeScope Container { get; set; }

        public WebApplicationService(HttpConfiguration config, ILifetimeScope container)
        {
            _config = config;
            _config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            Container = container;
        }

        private const string BaseUri = "http://*:8080";
        //private const string BaseUri = "https://*:8081";

        private IDisposable _service;

        public void Start()
        {
            _service = WebApp.Start(BaseUri, app =>
            {
                Log.Debug($"Starting OWIN web application on {BaseUri}");
                
                var listener = (HttpListener)app.Properties["System.Net.HttpListener"];
                listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

                Log.Debug(" .. using Cors");
                app.UseCors(CorsOptions.AllowAll);

                Log.Debug(" .. using autofac middleware");
                app.UseAutofacMiddleware(Container);

                Log.Debug(" .. using autofacwebapi");
                app.UseAutofacWebApi(_config);

                Log.Debug(" .. using webapi");
                app.UseWebApi(_config);

                Log.Debug(" .. mapping signalr");
                app.MapSignalR("/signalr", new HubConfiguration
                {
                    EnableDetailedErrors = true
                });
            });
            
        }

        public void Stop()
        {
            _service.Dispose();
        }


        private const string ServiceNameConfigurationKey = "ServiceName";
        private const string ServiceDisplayNameConfigurationKey = "ServiceDisplayName";
        private const string ServiceDescriptionConfigurationKey = "ServiceDescription";

        public string DisplayName => ConfigurationManager.AppSettings[ServiceDisplayNameConfigurationKey];
        public string ServiceName => ConfigurationManager.AppSettings[ServiceNameConfigurationKey];
        public string Description => ConfigurationManager.AppSettings[ServiceDescriptionConfigurationKey];
    }
}