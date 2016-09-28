using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Service.Web.Swagger
{
    public class SwaggerConfig : SwaggerConfigBase
    {

        public Assembly Assembly => typeof(SwaggerConfig).Assembly;

        protected override Func<HttpRequestMessage, string> RootUrlResolver { get; } = message => "test";

        protected override IEnumerable<string> XmlCommentPaths
        {
            get
            {
                yield return GetAssemblyXmlCommentsPath(Assembly);
                //yield return GetAssemblyXmlCommentsPath(typeof(Data.Transfer.ApiVersion).Assembly);
            }
        }

        public static void RegisterConfig(HttpConfiguration configuration) => new SwaggerConfig().ConfigureSwagger(configuration);

    }
}
