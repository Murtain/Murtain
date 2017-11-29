using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uranus.Configuration.Models;

namespace Uranus.Configuration.Provider
{
    /// <summary>
    /// Implements default behavior for ISettingStore.
    /// Only <see cref="GetSettingAsync"/> method is implemented and it gets setting's value
    /// from application's configuration file if exists, or returns null if not.
    /// </summary>
    public class DefaultSettingStore : IGlobalSettingStore
    {
        private readonly IConfigurationProvider configuration;
        public DefaultSettingStore(IConfigurationProvider configuration)
        {
            this.configuration = configuration;
        }

        public Task<GlobalSetting> GetSettingAsync(string name)
        {
            return Task.FromResult(new Models.GlobalSetting { Name = name, Value = ConfigurationManager.AppSettings[name] });
        }
        public Task DeleteSettingAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdateSettingAsync(GlobalSetting setting)
        {
            throw new NotImplementedException();
        }

        public Task<List<GlobalSetting>> GetAllSettingsAsync()
        {
            var settings = new List<GlobalSetting>();
            foreach (var key in configuration.Providers)
            {
                settings.Add(new Models.GlobalSetting { Name = key, Value = ConfigurationManager.AppSettings[key] });
            }
            return Task.FromResult(settings);
        }
    }
}
