using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DeFiPulse.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<DeFiPulseWebModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
