using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Murtain.Extensions;
using Xunit;

namespace XUnit.Murtain.Extensions
{
    public class EnumExtensions
    {
        public enum XUnitEnum
        {
            [Description("ENUM_1")]
            ENUM_1,
            ENUM_2
        }

        [Fact]
        public void When_Enum_TryGetName()
        {
            Assert.Equal("ENUM_1", XUnitEnum.ENUM_1.TryGetName());
        }

        [Fact]
        public void When_Enum_TryGetDescription()
        {
            Assert.Equal("ENUM_1", XUnitEnum.ENUM_1.TryGetDescription());
        }
    }
}
