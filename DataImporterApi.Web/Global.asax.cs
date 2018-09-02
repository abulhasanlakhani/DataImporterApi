using DataImporter.Business;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace DataImporterApi.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitializeIocContainer();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        public void InitializeIocContainer()
        {
            var container = new Container();

            container.Register<ITaxCalculator, TaxCalculator>(Lifestyle.Singleton);
            container.Register<IExtractor, Extractor>(Lifestyle.Singleton);
            container.Register<IValidationService, ValidationService>(Lifestyle.Singleton);
            container.Register<IMappingService, MappingService>(Lifestyle.Singleton);
            container.Register<IApplicationService, ApplicationService>(Lifestyle.Singleton);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = 
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
