using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.Caching.Configuration;
using Murtain.Caching.Models;
using Murtain.Extensions;

namespace Murtain.Caching
{
    public class CacheSettingManager : ICacheSettingManager
    {
        private readonly ICacheManager cacheManager;
        private readonly IServiceProvider serviceProvider;
        private readonly ICacheSettingsConfiguration settingsConfiguration;

        public CacheSettingManager(IServiceProvider serviceProvider, ICacheManager cacheManager, ICacheSettingsConfiguration settingsConfiguration)
        {
            this.cacheManager = cacheManager;
            this.serviceProvider = serviceProvider;
            this.settingsConfiguration = settingsConfiguration;
        }

        private IEnumerable<Models.CacheSetting> GlobalSettings
        {
            get
            {
                return cacheManager.Retrive<IEnumerable<Models.CacheSetting>>(settingsConfiguration.CacheSettingName, () =>
                {

                    var temp = new List<Models.CacheSetting>();

                    foreach (var providerType in settingsConfiguration.Providers)
                    {
                        var provider = CreateCacheSettingProvider(providerType);
                        foreach (var s in provider.GetCacheSettings())
                        {
                            temp.Add(s);
                        }
                    }

                    return temp;
                });
            }
        }
        public Task<IEnumerable<CacheSetting>> GetAsync()
        {
            return Task.FromResult(GlobalSettings);
        }

        public Task<CacheSetting> GetAsync(string name)
        {
            return Task.FromResult(GlobalSettings.FirstOrDefault(x => x.Name == name));
        }

        public Task<TimeSpan?> GetTimeSpanAsync(string name)
        {
            return Task.FromResult(GlobalSettings.FirstOrDefault(x => x.Name == name)?.ExpiredTime);
        }

        public Task<string> GetValueAsync(string name)
        {
            return Task.FromResult(GlobalSettings.FirstOrDefault(x => x.Name == name)?.Value);
        }

        private CacheSettingProvider CreateCacheSettingProvider(Type providerType)
        {
            return serviceProvider.GetService(providerType).TryAs<CacheSettingProvider>();
        }
    }
}
