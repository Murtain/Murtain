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
using Murtain.Angela.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Murtain.Angela
{
    public class Startup
    {
        private const string GLOBAL_SETTING_CACHE_NAME = "XUNIT_Murtain_ENTITYFRAMEWORK:GLOBAL_SETTINGS";
        private const string CACHE_SETTING_NAME = "XUNIT_Murtain_ENTITYFRAMEWORK:CACHE_SETTINGS";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();


            services.AddDbContext<ModelsContainer>(option =>
            {
                option.UseMySQL(@"Data Source=139.224.234.155;port=3306;Initial Catalog=hy;user id=root;password=scpark@123!dh8;");
            });

            services.AddMurtain()
                    .AddApplicationService()
                    .AddUnitOfWork()
                    .AddCacheManager()
                    .AddGlobalSettingManager()
                    .AddAutoMapper()
                    .AddEntityFrameWork();

            System.Diagnostics.Debug.WriteLine("   　 へ　　　　　／|");
            System.Diagnostics.Debug.WriteLine("   　/＼7　　∠＿/");
            System.Diagnostics.Debug.WriteLine("    /　│　　 ／　／");
            System.Diagnostics.Debug.WriteLine("   │　Z ＿,＜　／　　 /`ヽ");
            System.Diagnostics.Debug.WriteLine("   │　　　　　ヽ　　 /　　〉");
            System.Diagnostics.Debug.WriteLine("    Y　　　　　 `　 /　　/");
            System.Diagnostics.Debug.WriteLine("   ｲ  ●　､　● ⊂⊃〈　　/");
            System.Diagnostics.Debug.WriteLine("   ()　 へ　　　　|　＼〈");
            System.Diagnostics.Debug.WriteLine("   　>ｰ ､_　 ィ　 │ ／／");
            System.Diagnostics.Debug.WriteLine("    / へ　　 /　ﾉ＜| ＼＼");
            System.Diagnostics.Debug.WriteLine("    ヽ_ﾉ　　(_／　 │／／");
            System.Diagnostics.Debug.WriteLine("   　7　　　　　　　|／");
            System.Diagnostics.Debug.WriteLine("   　＞―r￣￣`ｰ―＿");

            foreach (var service in services.Where(x => x.ServiceType.ToString().StartsWith("Murtain")))
            {
                System.Diagnostics.Debug.WriteLine(service.ServiceType.ToString(), "【Murtain->Setup->ConfigureServices】");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMurtain()
               .ConfigureAutoMapper();

            app.UseMvc();
        }
    }
}
