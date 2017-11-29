using System;
using System.Collections.Generic;
using System.Text;
using Uranus.Extensions;
using Xunit;

namespace XUnit.Uranus.Extensions
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
