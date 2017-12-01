using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Uranus.Configuration;
using Uranus.GlobalSetting.Provider;
using Uranus.GlobalSetting.Models;
using Uranus.Builder;
using Uranus.Email;

namespace XUnit.Uranus.Caching
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddUranus()
                    .AddGlobalSettingManager(config =>
                    {
                        config.GlobalSettingCacheName = "MY_APPLICATION:GLOBAL_SETTINGS:";
                        config.Providers.Add<EmailGlobalSettingProvider>();
                    })
                    .AddCacheManager(config =>
                    {

                    });
            ;

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseUranus();
            app.UseMvc();
        }
    }




    public class Constants
    {

       
    }
}
