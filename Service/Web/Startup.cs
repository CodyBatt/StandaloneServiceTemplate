
using System.Web.Http;

namespace Service.Web
{
    public static class Startup
    {
        public static HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            return config;
        }
    }
}
