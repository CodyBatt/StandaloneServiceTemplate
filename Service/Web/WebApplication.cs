using Topshelf;

namespace Service.Web
{

    class WebApplication : IApplication
    {
        private readonly IService _service;

        public WebApplication(IService service)
        {
            _service = service;
        }

        public void Run()
        {
            // Topshelf config
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<IService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(() => _service);
                    serviceConfigurator.WhenStarted(myService => myService.Start());
                    serviceConfigurator.WhenStopped(myService => myService.Stop());
                });

                hostConfigurator.RunAsLocalSystem();

                hostConfigurator.SetDisplayName(_service.DisplayName);
                hostConfigurator.SetDescription(_service.Description);
                hostConfigurator.SetServiceName(_service.ServiceName);
            });
        }
    }
}
