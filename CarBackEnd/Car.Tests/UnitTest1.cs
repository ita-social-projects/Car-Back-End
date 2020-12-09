using System;
using Xunit;

namespace Car.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int actual = 1;
            int expected = 1;

            Assert.Equal(expected, actual);
        }
    }
}
