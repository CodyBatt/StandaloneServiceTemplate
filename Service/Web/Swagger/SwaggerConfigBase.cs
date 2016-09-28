using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle;
using Swashbuckle.Application;

namespace Service.Web.Swagger
{
    public abstract class SwaggerConfigBase
    {

        protected virtual Func<HttpRequestMessage, string> RootUrlResolver { get; } = DefaultRootUrlResolver;

        protected abstract IEnumerable<string> XmlCommentPaths { get; }

        public void ConfigureSwagger(HttpConfiguration httpConfig)
        {
            httpConfig.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Test Service API");
                    
                    c.RootUrl(req =>
                    {
                        var result = req.RequestUri.GetLeftPart(UriPartial.Authority) +
                                     req.GetRequestContext().VirtualPathRoot.TrimEnd('/');
                        return result;
                    });

                    foreach (var commentPath in (XmlCommentPaths ?? Enumerable.Empty<string>()))
                    {
                        c.IncludeXmlComments(commentPath);
                    }
                })
                .EnableSwaggerUi(c =>
                {
                    
                });
        }

        protected static string DefaultRootUrlResolver(HttpRequestMessage req)
        {
            return req.RequestUri.GetLeftPart(UriPartial.Authority) + req.GetConfiguration().VirtualPathRoot.TrimEnd('/');
        }

        protected static string GetAssemblyName(Assembly assembly) => assembly.GetName().Name;

        protected static string GetAssemblyXmlCommentsPath(Assembly assembly)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = $"{GetAssemblyName(assembly)}.XML";
            var binPath = Path.Combine(basePath, "bin", fileName);
            return File.Exists(binPath)
                ? binPath
                : Path.Combine(basePath, fileName);
        }
    }
}
