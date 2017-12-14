using System;
using System.Collections.Generic;
using System.Text;
using Murtain.Collections;
using Murtain.GlobalSetting.Provider;

namespace Murtain.GlobalSetting.Configuration
{
    public class GlobalSettingConfiguration : IGlobalSettingConfiguration
    {

        public GlobalSettingConfiguration()
        {
            this.GlobalSettingCacheName = "GlobalSettings";
            this.Providers = new TypeList<GlobalSettingProvider>();
        }

        public string GlobalSettingCacheName { get; set; }


        /// <summary>
        /// List of settings providers.
        /// </summary>
        public ITypeList<GlobalSettingProvider> Providers { get; }

    }
}
