using System;
using System.Collections.Generic;
using System.Text;
using Murtain.Caching.Models;

namespace Murtain.Caching
{
    public abstract class CacheSettingProvider
    {
        public abstract IEnumerable<CacheSetting> GetCacheSettings();
    }
}
