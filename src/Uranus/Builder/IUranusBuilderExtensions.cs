using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Uranus.Caching;
using Uranus.Caching.Configuration;
using Uranus.Configuration;
using Uranus.Email;
using Uranus.GlobalSetting;
using Uranus.GlobalSetting.Configuration;
using Uranus.GlobalSetting.Provider;
using Uranus.GlobalSetting.Store;
using Uranus.Session;

namespace Uranus.Builder
{
    public static class IUranusBuilderExtensions
    {

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
                scan.FromApplicationDependencies()
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
                scan.FromApplicationDependencies()
                          .AddClasses(c => c.AssignableTo(typeof(GlobalSettingProvider)))
                          .AsSelf()
                          .WithSingletonLifetime();
            });

            return builder;
        }

        public static IUranusBuilder AddIUserSession(this IUranusBuilder builder)
        {
            builder.Services.AddTransient<IUserSession, ClaimsUserSession>();
            builder.Services.AddSingleton<EmailGlobalSettingProvider>();
            return builder;
        }

    }
}
