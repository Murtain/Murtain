using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Uranus.Extensions;

namespace XUnit.Uranus.Extensions
{
    public class ICollectionExtensions
    {
        [Fact]
        public void When_Collection_IsNullOrEmpty()
        {
            Assert.Equal(true, ((ICollection<string>)new HashSet<string>()).IsNullOrEmpty());
            Assert.Equal(false, ((ICollection<string>)new HashSet<string>() { "A", "B", "C" }).IsNullOrEmpty());
        }


        [Fact]
        public void When_Collection_AddIfNotContains()
        {
            Assert.Equal(true, ((ICollection<string>)new HashSet<string>() { "A", "B", "C" }).AddIfNotContains("D"));
            Assert.Equal(false, ((ICollection<string>)new HashSet<string>() { "A", "B", "C" }).AddIfNotContains("C"));
        }

    }
}
