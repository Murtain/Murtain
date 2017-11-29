using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uranus.Extensions;
using Xunit;

namespace XUnit.Uranus.Extensions
{
    public class IQueryableExtensions
    {
        [Fact]
        public void When_IQueryable_WhereIf()
        {
            Assert.Equal(2, (new List<string> { "A", "B" }).AsQueryable<string>().WhereIf(false, x => x == "A").Count());
        }
    }
}
