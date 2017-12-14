using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Murtain.GlobalSetting.Models;

namespace Murtain.GlobalSetting.Store
{
    public class GlobalSettingConfigurationStore : IGlobalSettingStore
    {

        public async Task AddOrUpdateAsync(Models.GlobalSetting setting)
        {
            await Task.FromResult(0);
        }

        public async Task DeleteAsync(string name)
        {
            await Task.FromResult(0);
        }

        public async Task<Models.GlobalSetting> GetAsync(string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            return await Task.FromResult(new Models.GlobalSetting(name, value));
        }

        public async Task<IEnumerable<Models.GlobalSetting>> GetAsync()
        {
            return await Task.FromResult<IEnumerable<Models.GlobalSetting>>(null);
        }

        public async void MigrationAsync(IEnumerable<Models.GlobalSetting> settings)
        {
            await Task.FromResult(0);
        }
    }
}
