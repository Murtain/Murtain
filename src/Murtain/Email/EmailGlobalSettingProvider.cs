using System;
using System.Collections.Generic;
using System.Text;
using Murtain.GlobalSetting.Provider;

namespace Murtain.Email
{
    public class EmailGlobalSettingProvider : GlobalSettingProvider
    {
        public override IEnumerable<GlobalSetting.Models.GlobalSetting> GetSettings()
        {
            return new List<GlobalSetting.Models.GlobalSetting>
            {
                new GlobalSetting.Models.GlobalSetting("端口",Constants.Email.Port,"25","邮箱配置","邮件发送端口",GlobalSetting.Models.GlobalSettingScope.Application),
                new GlobalSetting.Models.GlobalSetting("发件人邮箱",Constants.Email.Port,"admin@x-dva.com","邮箱配置","发件人邮箱",GlobalSetting.Models.GlobalSettingScope.Application),
            };
        }
    }
}
