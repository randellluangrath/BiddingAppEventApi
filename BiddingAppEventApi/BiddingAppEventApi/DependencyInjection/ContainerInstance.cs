using System;
using System.Linq;
using System.Web.Http;
using WebAppEventApi.Logging;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace WebAppEventApi.DependencyInjection
{
    // This class provides a static instance of the Simple Injector container
    // and it registers all interfaces as transient or singleton
    public static class ContainerInstance
    {
        private static Container _container;

        public static Container Container => _container ?? (_container = new Container());

        public static void RegisterInterfaces()
        {
            Logger.Info("Registering interfaces to DI container");

            try
            {
                RegisterScopedClasses();

                RegisterWebApiControllers();
            }
            catch (Exception ex)
            {
                Logger.Error("An error occurred while attempting to update register interfaces.");
                Logger.Error($"Class: { nameof(ContainerInstance)}, Method: {nameof(RegisterInterfaces) }");
                Logger.Error(ex);
            }
        }

        private static void RegisterScopedClasses()
        {
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            var assembly = typeof(ContainerInstance).Assembly;

            var transientRegistrations =
                from type in assembly.GetExportedTypes()
                where type.GetInterfaces().Any(x => x.Assembly == assembly)
                select new { Service = type.GetInterfaces().Single(), Implementation = type };

            foreach (var reg in transientRegistrations)
            {
                Container.Register(reg.Service, reg.Implementation, Lifestyle.Scoped);
            }
        }

        private static void RegisterWebApiControllers()
        {
            Container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(Container);
        }
    }
}
