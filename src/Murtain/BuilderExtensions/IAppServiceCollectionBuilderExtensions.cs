using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Murtain.Builder;
using Murtain.Caching;
using Murtain.Caching.Configuration;
using Murtain.Configuration;
using Murtain.Domain;
using Murtain.Domain.UnitOfWork;
using Murtain.Domain.UnitOfWork.Configuration;
using Murtain.Domain.UnitOfWork.Provider;
using Murtain.Email;
using Murtain.GlobalSetting;
using Murtain.GlobalSetting.Configuration;
using Murtain.GlobalSetting.Provider;
using Murtain.GlobalSetting.Store;
using Murtain.Session;
using Murtain.Collections;
using Murtain.Dependency;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IAppServiceCollectionBuilderExtensions
    {
        public static IAppServiceCollectionBuilder AddDependency(this IAppServiceCollectionBuilder builder)
        {
            builder.Services.Scan(scan =>
            {
                scan.FromAssemblies(AssemblyLoader.GetAssemblies())
                          .AddClasses(c => c.Where(t => typeof(ITransientDependency).IsAssignableFrom(t) && t != typeof(ITransientDependency) && !t.IsAbstract))
                          .AsImplementedInterfaces()
                          .WithTransientLifetime();


                scan.FromAssemblies(AssemblyLoader.GetAssemblies())
                          .AddClasses(c => c.Where(t => typeof(ISingletonDependency).IsAssignableFrom(t) && t != typeof(ISingletonDependency) && !t.IsAbstract))
                          .AsImplementedInterfaces()
                          .WithSingletonLifetime();


                scan.FromAssemblies(AssemblyLoader.GetAssemblies())
                          .AddClasses(c => c.Where(t => typeof(IApplicationService).IsAssignableFrom(t) && t != typeof(IApplicationService) && !t.IsAbstract))
                          .AsImplementedInterfaces()
                          .WithTransientLifetime();


                scan.FromAssemblies(AssemblyLoader.GetAssemblies())
                          .AddClasses(c => c.AssignableTo(typeof(IRepository)))
                          .AsImplementedInterfaces()
                          .WithTransientLifetime();

            });
            return builder;
        }

        public static IAppServiceCollectionBuilder AddUnitOfWork(this IAppServiceCollectionBuilder builder, Action<IUnitOfWorkConfiguration> invoke = null)
        {
            builder.Services.AddSingleton<IUnitOfWorkConfiguration>(provider =>
            {
                var c = new UnitOfWorkConfiguration();
                invoke?.Invoke(c);
                return c;
            });

            builder.Services.AddTransient<IUnitOfWorkProvider, UnitOfWorkProvider>();
            builder.Services.AddTransient<IUnitOfWorkManager, UnitOfWorkManager>();

            return builder;
        }

        public static IAppServiceCollectionBuilder AddLoggerInterception(this IAppServiceCollectionBuilder builder)
        {
            builder.Services.AddInterception();
            return builder;
        }

        public static IAppServiceCollectionBuilder AddCacheManager(this IAppServiceCollectionBuilder builder, Action<ICacheSettingsConfiguration> invoke = null)
        {
            builder.Services.AddSingleton<ICacheSettingsConfiguration>(provider =>
            {
                var c = new CacheSettingsConfiguration();
                invoke?.Invoke(c);
                return c;
            });

            builder.Services.AddSingleton<ICacheSettingManager, CacheSettingManager>();
            builder.Services.AddSingleton<ICacheSettingsConfiguration, CacheSettingsConfiguration>();
            builder.Services.AddSingleton<ICacheManager, MemoryCacheManager>();

            builder.Services.Scan(scan =>
            {
                scan.FromAssemblies(AssemblyLoader.GetAssemblies())
                          .AddClasses(c => c.AssignableTo(typeof(CacheSettingProvider)))
                          .AsSelf()
                          .WithSingletonLifetime();
            });

            return builder;
        }

        public static IAppServiceCollectionBuilder AddGlobalSettingManager(this IAppServiceCollectionBuilder builder, Action<IGlobalSettingConfiguration> invoke = null)
        {
            builder.Services.AddSingleton<IGlobalSettingConfiguration>(provider =>
            {
                var c = new GlobalSettingConfiguration();
                invoke?.Invoke(c);
                return c;
            });
            builder.Services.AddTransient<IGlobalSettingStore, GlobalSettingConfigurationStore>();
            builder.Services.AddSingleton<IGlobalSettingManager, GlobalSettingManager>();

            builder.Services.Scan(scan =>
            {
                scan.FromAssemblies(AssemblyLoader.GetAssemblies())
                          .AddClasses(c => c.AssignableTo(typeof(GlobalSettingProvider)))
                          .AsSelf()
                          .WithSingletonLifetime();
            });

            return builder;
        }

        public static IAppServiceCollectionBuilder AddClaimsUserSession(this IAppServiceCollectionBuilder builder)
        {
            builder.Services.AddTransient<IUserSession, ClaimsUserSession>();
            return builder;
        }

    }
}
