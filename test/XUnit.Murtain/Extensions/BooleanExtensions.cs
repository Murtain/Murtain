using System;
using System.Collections.Generic;
using System.Text;
using Murtain.Extensions;
using Xunit;

namespace XUnit.Murtain.Extensions
{
    public class BooleanExtensions
    {
        [Fact]
        public void WhenBooleanToLowerString_()
        {
            Assert.Equal("true", Boolean.Parse("TRUE").ToLowerString());
        }
        [Fact]
        public void WhenBooleanToUpperString_()
        {
            Assert.Equal("TRUE", Boolean.Parse("TRUE").ToUpperString());
        }
    }
}
