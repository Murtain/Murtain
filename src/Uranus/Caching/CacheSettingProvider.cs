using System;
using System.Collections.Generic;
using System.Text;
using Uranus.Caching.Models;

namespace Uranus.Caching
{
    public abstract class CacheSettingProvider
    {
        public abstract IEnumerable<CacheSetting> GetCacheSettings();
    }
}
