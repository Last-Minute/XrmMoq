using Microsoft.Xrm.Sdk;
using System;

namespace TestPlugin2016
{
    public class ExamplePlugin5 : IPlugin, IExamplePlugin5
    {
        private readonly string _unsecureConfig;
        private readonly string _secureConfig;
        public ExamplePlugin5(string unsecureConfig, string secureConfig)
        {
            _unsecureConfig = unsecureConfig;
            _secureConfig = secureConfig;
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            try
            {
                tracer.Trace("Unsecure: " + _unsecureConfig);
                tracer.Trace("Secure: " + _secureConfig);
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        public int Test()
        {
            var x = Test2() / 2;

            return x;
        }

        public virtual int Test2()
        {
            var x = 10;

            return x;
        }

        public string Test3()
        {
            string unsecureConfig = _unsecureConfig;

            return unsecureConfig;
        }
    }

    public interface IExamplePlugin5
    {
        int Test();
    }
}