using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Uranus.Builder;
using Uranus.Caching;
using Uranus.Caching.Configuration;
using Uranus.Configuration;
using Uranus.Domain;
using Uranus.Domain.UnitOfWork;
using Uranus.Domain.UnitOfWork.Configuration;
using Uranus.Domain.UnitOfWork.Provider;
using Uranus.Email;
using Uranus.GlobalSetting;
using Uranus.GlobalSetting.Configuration;
using Uranus.GlobalSetting.Provider;
using Uranus.GlobalSetting.Store;
using Uranus.Session;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IUranusBuilderExtensions
    {

        public static IUranusBuilder AddUranusUnitOfWork(this IUranusBuilder builder,  Action<IUnitOfWorkConfiguration> setupAction)
        {
            builder.Services.AddSingleton<IUnitOfWorkConfiguration>(provider =>
            {
                var c = new UnitOfWorkConfiguration();
                setupAction(c);
                return c;
            });

            builder.Services.AddTransient<IUnitOfWorkProvider, UnitOfWorkProvider>();
            builder.Services.AddTransient<IUnitOfWorkManager, UnitOfWorkManager>();

            return builder;
        }
        public static IUranusBuilder AddUranusApplicationService(this IUranusBuilder builder, params Assembly[] assembly)
        {

            builder.Services.Scan(scan =>
            {
                scan.FromAssemblies(assembly)
                          .AddClasses(c => c.Where(t => typeof(IApplicationService).IsAssignableFrom(t) && t != typeof(IApplicationService) && !t.IsAbstract))
                          .AsImplementedInterfaces()
                          .WithTransientLifetime();


                scan.FromAssemblies(assembly)
                          .AddClasses(c => c.AssignableTo(typeof(IRepository)))
                          .AsImplementedInterfaces()
                          .WithTransientLifetime();

            });


            return builder;
        }

        public static IUranusBuilder AddCacheManager(this IUranusBuilder builder, Action<ICacheSettingsConfiguration> setupAction)
        {
            builder.Services.AddSingleton<ICacheSettingsConfiguration>(provider =>
            {
                var c = new CacheSettingsConfiguration();
                setupAction(c);
                return c;
            });

            builder.Services.AddSingleton<ICacheSettingManager, CacheSettingManager>();
            builder.Services.AddSingleton<ICacheSettingsConfiguration, CacheSettingsConfiguration>();
            builder.Services.AddSingleton<ICacheManager, MemoryCacheManager>();

            builder.Services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(CacheSettingProvider))
                          .AddClasses(c => c.AssignableTo(typeof(CacheSettingProvider)))
                          .AsSelf()
                          .WithSingletonLifetime();
            });


            return builder;
        }

        public static IUranusBuilder AddGlobalSettingManager(this IUranusBuilder builder, Action<IGlobalSettingConfiguration> setupAction)
        {
            builder.Services.AddSingleton<IGlobalSettingConfiguration>(provider =>
            {
                var c = new GlobalSettingConfiguration();
                setupAction(c);
                return c;
            });
            builder.Services.AddTransient<IGlobalSettingStore, GlobalSettingConfigurationStore>();
            builder.Services.AddSingleton<IGlobalSettingManager, GlobalSettingManager>();

            builder.Services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(GlobalSettingProvider))
                          .AddClasses(c => c.AssignableTo(typeof(GlobalSettingProvider)))
                          .AsSelf()
                          .WithSingletonLifetime();
            });

            return builder;
        }

        public static IUranusBuilder AddClaimsUserSession(this IUranusBuilder builder)
        {
            builder.Services.AddTransient<IUserSession, ClaimsUserSession>();
            return builder;
        }

    }
}
