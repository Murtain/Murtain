using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Murtain.Caching.Models;

namespace Murtain.Caching
{
    public interface ICacheSettingManager
    {
        Task<CacheSetting> GetAsync(string name);
        Task<string> GetValueAsync(string name);
        Task<TimeSpan?> GetTimeSpanAsync(string name);
        Task<IEnumerable<CacheSetting>> GetAsync();
    }
}
