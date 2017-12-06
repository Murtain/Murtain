using System;
using System.Collections.Generic;
using System.Text;
using Uranus.Collections;

namespace Uranus.Caching.Configuration
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
