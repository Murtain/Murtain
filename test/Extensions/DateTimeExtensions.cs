using System;
using System.Collections.Generic;
using System.Text;
using Murtain.Extensions;
using Xunit;

namespace XUnit.Murtain.Extensions
{
    public class DateTimeExtensions
    {
        [Fact]
        public void When_DateTime_Null_TryGetValue()
        {
            DateTime? dateTime = null;
            Assert.Equal(default(DateTime), dateTime.TryGetValue());
        }

        [Fact]
        public void When_DateTime_2017_11_01_FirstDayOfTheMonth()
        {
            Assert.Equal(DateTime.Parse("2017-11-01"), DateTime.Parse("2017-11-29").FirstDayOfTheMonth());
        }

        [Fact]
        public void When_DateTime_2000_02_01_LastDayOfTheMonth()
        {
            Assert.Equal(DateTime.Parse("2000-02-29"), DateTime.Parse("2000-02-01").LastDayOfTheMonth());
        }

        [Fact]
        public void When_DateTime_SetHour()
        {
            Assert.Equal(DateTime.Parse("2000-02-29 20:00:00"), DateTime.Parse("2000-02-29").SetTime(20));
        }

        [Fact]
        public void When_DateTime_Set_Hour_Minite()
        {
            Assert.Equal(DateTime.Parse("2000-02-29 20:59:00"), DateTime.Parse("2000-02-29").SetTime(20, 59));
        }

        [Fact]
        public void When_DateTime_Set_Hour_Minite_Second()
        {
            Assert.Equal(DateTime.Parse("2000-02-29 20:59:59"), DateTime.Parse("2000-02-29").SetTime(20, 59, 59));
        }

        [Fact]
        public void When_DateTime_Set_Hour_Minite_Second_Millisecond()
        {
            Assert.Equal(DateTime.Parse("2000-02-29 20:59:59").AddMilliseconds(59), DateTime.Parse("2000-02-29").SetTime(20, 59, 59, 59));
        }


        [Fact]
        public void When_DateTime_2017_11_29_Before_2017_12_01()
        {
            Assert.Equal(true, DateTime.Parse("2017-11-29").IsBefore(DateTime.Parse("2017-12-01")));
        }

        [Fact]
        public void When_DateTime_2017_11_29_After_2017_11_01()
        {
            Assert.Equal(true, DateTime.Parse("2017-11-29").IsAfter(DateTime.Parse("2017-11-01")));
        }

        [Fact]
        public void When_DateTime_2017_11_29_Equals_2017_11_29()
        {
            Assert.Equal(true, DateTime.Parse("2017-11-29").IsBeforeOrEqual(DateTime.Parse("2017-11-29")));
            Assert.Equal(true, DateTime.Parse("2017-11-29").IsAfterOrEqual(DateTime.Parse("2017-11-29")));
        }

    }
}
