using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uranus.Caching.Models;

namespace Uranus.Caching
{
    public interface ICacheSettingManager
    {
        Task<CacheSetting> GetAsync(string name);
        Task<string> GetValueAsync(string name);
        Task<TimeSpan?> GetTimeSpanAsync(string name);
        Task<IEnumerable<CacheSetting>> GetAsync();
    }
}
