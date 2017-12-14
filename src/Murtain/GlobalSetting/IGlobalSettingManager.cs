using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.GlobalSetting
{
    public interface IGlobalSettingManager
    {
        /// <summary>
        /// Gets the <see cref="GlobalSetting"/> object with given unique name.
        /// Throws exception if can not find the setting.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>The <see cref="GlobalSetting"/> object.</returns>
        Task<Models.GlobalSetting> GetValueAsync(string name);
        /// <summary>
        /// Gets current value of a setting.
        /// It gets the setting value, overwritten by application if exists.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Current value of the setting</returns>
        Task<string> GetValueAsync(string name, Models.GlobalSettingScope scope = Models.GlobalSettingScope.Application);
        /// <summary>
        /// Gets a list of all setting.
        /// </summary>
        /// <returns>All settings.</returns>
        Task<IEnumerable<Models.GlobalSetting>> GetAsync();
        /// <summary>
        /// Add Or Update setting for the application level.
        /// </summary>
        /// <param name="data">save data</param>
        /// <returns></returns>
        Task AddOrUpdateAsync(Models.GlobalSetting data);
        /// <summary>
        /// Delete setting by name.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>Value of the setting</returns>
        Task DeleteAsync(string name);
        /// <summary>
        /// Clear caches.
        /// </summary>
        Task ClearGlobalSettingCacheAsync();
    }
}
