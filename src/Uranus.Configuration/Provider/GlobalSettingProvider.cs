using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

using Uranus.Configuration.Models;

namespace Uranus.Configuration.Provider
{
    public abstract class GlobalSettingProvider : IGlobalSettingProvider
    {
        /// <summary>
        /// Gets all setting definitions provided by this provider.
        /// </summary>
        /// <returns>List of settings</returns>
        public abstract IEnumerable<GlobalSetting> GetSettings();
    }
}
