using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Uranus.Configuration.Models;

namespace Uranus.Configuration.Provider
{
    public interface IGlobalSettingProvider
    {
        IEnumerable<GlobalSetting> GetSettings();
    }
}
