using System;
using CarBackEnd;
using Xunit;
using FluentAssertions;

namespace Car.Tests
{
    public class ProgramTest
    {
        [Fact]
        public void TestWebHostBuilder()
        {
            Action action = () => Program.CreateWebHostBuilder(new[] { "1" });
            action.Should().NotThrow();
        }
    }
}