using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


using Uranus.GlobalSetting.Models;

namespace Uranus.GlobalSetting
{
    /// <summary>
    /// This interface is used to get/set settings from/to a data source (database).
    /// </summary>
    public interface IGlobalSettingStore
    {
        /// <summary>
        /// Gets a setting or null.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        /// <returns>Setting object</returns>
        Task<Models.GlobalSetting> GetAsync(string name);

        /// <summary>
        /// Gets a list of setting.
        /// </summary>
        /// <returns>List of settings</returns>
        Task<IEnumerable<Models.GlobalSetting>> GetAsync();

        /// <summary>
        /// Adds a setting.
        /// </summary>
        /// <param name="setting">Setting to add</param>
        Task AddOrUpdateAsync(Models.GlobalSetting setting);

        /// <summary>
        /// Deletes a setting.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        Task DeleteAsync(string name);

        /// <summary>
        /// Persistence settings
        /// </summary>
        /// <param name="settings"></param>
        void MigrationAsync(IEnumerable<Models.GlobalSetting> settings);

    }
}
