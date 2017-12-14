using System;
using System.Collections.Generic;
using System.Text;
using Murtain.Collections;

namespace Murtain.Caching.Configuration
{
    public interface ICacheSettingsConfiguration
    {
        string CacheSettingName { get; set; }
        /// <summary>
        /// List of settings providers.
        /// </summary>
        ITypeList<CacheSettingProvider> Providers { get; }
    }
}
