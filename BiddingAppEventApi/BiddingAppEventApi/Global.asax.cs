using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using WebAppEventApi.Attributes;
using WebAppEventApi.DependencyInjection;
using WebAppEventApi.Utilities;
using System.Data.Entity;

namespace WebAppEventApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ContainerInstance.RegisterInterfaces();
            RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);

        }

        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            filters.Add(new HttpAuthorizeAttribute(new ConfigurationUtility()));
            filters.Add(new HttpExceptionAttribute());
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
