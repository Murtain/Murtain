using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using Uranus.Extensions;

namespace XUnit.Uranus.Extensions
{
    public class DictionaryExtensions
    {
        [Fact]
        public void When_Dictionary_TryGetValueOrDefault()
        {
            var key = "NOT_EXIST_KEY";

            Assert.Equal(null, new Dictionary<string, string>().TryGetValueOrDefault(key));
            Assert.Equal(0, new Dictionary<string, int>().TryGetValueOrDefault(key));
            Assert.Equal(null, new Dictionary<string, int?>().TryGetValueOrDefault(key));
            Assert.Equal(false, new Dictionary<string, bool>().TryGetValueOrDefault(key));
            Assert.Equal(default(Decimal), new Dictionary<string, decimal>().TryGetValueOrDefault(key));
        }


    }
}
