using System;
using System.Collections.Generic;
using System.Text;
using Murtain.Collections;

namespace Murtain.Caching.Configuration
{
    public class CacheSettingsConfiguration : ICacheSettingsConfiguration
    {
        public string CacheSettingName { get; set; }

        public ITypeList<CacheSettingProvider> Providers { get; set; }

        public CacheSettingsConfiguration()
        {
            Providers = new TypeList<CacheSettingProvider>();
        }
    }
}
