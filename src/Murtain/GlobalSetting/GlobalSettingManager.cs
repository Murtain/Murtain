using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.Caching;
using Murtain.Extensions;
using Murtain.GlobalSetting.Configuration;
using Murtain.GlobalSetting.Models;
using Murtain.GlobalSetting.Provider;
using Murtain.GlobalSetting.Store;

namespace Murtain.GlobalSetting
{
    public class GlobalSettingManager : IGlobalSettingManager
    {
        private readonly IGlobalSettingStore globalSettingStore;
        private readonly IGlobalSettingConfiguration globalSettingConfiguration;
        private readonly ICacheManager cacheManager;
        private readonly IServiceProvider serviceProvider;


        public GlobalSettingManager(IGlobalSettingStore globalSettingStore, IServiceProvider serviceProvider, IGlobalSettingConfiguration globalSettingConfiguration, ICacheManager cacheManager)
        {
            this.globalSettingConfiguration = globalSettingConfiguration;
            this.cacheManager = cacheManager;
            this.serviceProvider = serviceProvider;
            this.globalSettingStore = globalSettingStore;

            ConfigurationLoadAsync();
        }


        private void ConfigurationLoadAsync()
        {
            globalSettingStore.MigrationAsync(GlobalSettings);
        }

        private IEnumerable<Models.GlobalSetting> GlobalSettings
        {
            get
            {
                return cacheManager.Retrive<IEnumerable<Models.GlobalSetting>>(globalSettingConfiguration.GlobalSettingCacheName, () =>
                {

                    var temp = new List<Models.GlobalSetting>();

                    foreach (var providerType in globalSettingConfiguration.Providers)
                    {
                        var provider = CreateGlobalSettingProvider(providerType);
                        foreach (var s in provider.GetSettings())
                        {
                            temp.Add(globalSettingStore.GetAsync(s.Name).Result);
                        }
                    }

                    return temp;
                });
            }
        }

        private GlobalSettingProvider CreateGlobalSettingProvider(Type providerType)
        {
            return serviceProvider.GetService(providerType).TryAs<GlobalSettingProvider>();
        }

        public async Task AddOrUpdateAsync(Models.GlobalSetting data)
        {
            await globalSettingStore.AddOrUpdateAsync(data);
        }

        public async Task ClearGlobalSettingCacheAsync()
        {
            cacheManager.Remove(globalSettingConfiguration.GlobalSettingCacheName);
            await Task.FromResult(0);
        }

        public async Task DeleteAsync(string name)
        {
            await globalSettingStore.DeleteAsync(name);
        }

        public async Task<IEnumerable<Models.GlobalSetting>> GetAsync()
        {
            return await Task.FromResult(GlobalSettings);
        }

        public async Task<Models.GlobalSetting> GetValueAsync(string name)
        {
            return await Task.FromResult(GlobalSettings.FirstOrDefault(x => x.Name == name));
        }

        public async Task<string> GetValueAsync(string name, GlobalSettingScope scope = GlobalSettingScope.Application)
        {
            return await Task.FromResult(GlobalSettings.FirstOrDefault(x => x.Name == name && x.Scope == scope)?.Value);
        }
    }
}
