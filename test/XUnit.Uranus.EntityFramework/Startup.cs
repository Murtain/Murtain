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
using Microsoft.EntityFrameworkCore;
using Uranus.Domain;
using System.Reflection;
using Uranus.EntityFramework.Provider;

namespace XUnit.Uranus.EntityFramework
{

    public class Startup
    {
        private const string GLOBAL_SETTING_CACHE_NAME = "XUNIT_URANUS_ENTITYFRAMEWORK:GLOBAL_SETTINGS";
        private const string CACHE_SETTING_NAME = "XUNIT_URANUS_ENTITYFRAMEWORK:CACHE_SETTINGS";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services
                    .AddDbContext<ModelsContainer>(option =>
                    {
                        option.UseMySQL(@"Data Source=139.224.234.155;port=3306;Initial Catalog=hy;user id=root;password=scpark@123!dh8;");
                    });

            services.AddUranus()
                    .AddUranusApplicationService(Assembly.GetAssembly(typeof(Startup)))
                    .AddUranusUnitOfWork(cfg => { })
                    .AddGlobalSettingManager(cfg =>
                    {
                        cfg.GlobalSettingCacheName = GLOBAL_SETTING_CACHE_NAME;
                    })
                    .AddCacheManager(cfg =>
                    {
                        cfg.CacheSettingName = CACHE_SETTING_NAME;
                    })
                    .AddClaimsUserSession()
                    .AddEntityFrameWork();

            var ss = new List<ServiceDescriptor>();
            foreach (var s in services)
            {
                if (s.ServiceType.FullName.Contains("Uranus"))
                {
                    ss.Add(s);
                }
            }


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
}
