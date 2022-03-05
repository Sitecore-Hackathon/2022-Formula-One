using Microsoft.Extensions.DependencyInjection;
using Hack2022.Foundation.SitecoreSend.Services;
using Sitecore.DependencyInjection;

namespace Hack2022.Foundation.SitecoreSend.Configurator
{
    public class RegisterDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection services)
        {
            services.AddSingleton(typeof(ISendService), typeof(SitecoreSendService));
        }
    }
}