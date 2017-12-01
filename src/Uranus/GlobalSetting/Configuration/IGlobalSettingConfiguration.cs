﻿using System;
using System.Collections.Generic;
using System.Text;
using Uranus.Collections;
using Uranus.GlobalSetting.Provider;

namespace Uranus.GlobalSetting.Configuration
{
    public interface IGlobalSettingConfiguration
    {
        /// <summary>
        /// Settings cache key , default `GlobalSettings`
        /// </summary>
        string GlobalSettingCacheName { get; set; }


        /// <summary>
        /// List of settings providers.
        /// </summary>
        ITypeList<GlobalSettingProvider> Providers { get; }
    }
}
