using System;
using Car.BLL.Exceptions;
using CarBackEnd;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Car.Tests
{
    public class StartupTest
    {
        private Mock<Startup> _startup;
        private Mock<IServiceCollection> _serviceCollection;
        private Mock<IApplicationBuilder> _applicationBuilder;
        private Mock<IWebHostEnvironment> _environment;

        public StartupTest()
        {
            _startup = new Mock<Startup>();
            _serviceCollection = new Mock<IServiceCollection>();
            _applicationBuilder = new Mock<IApplicationBuilder>();
            _environment = new Mock<IWebHostEnvironment>();
        }

        [Fact]
        public void TestConfigureServices()
        {
            Action action = () => _startup.Object.ConfigureServices(_serviceCollection.Object);
            action.Should().NotThrow<DefaultApplicationException>();
        }

        [Fact]
        public void TestConfigure()
        {
            Action action = () => _startup.Object.Configure(_applicationBuilder.Object, _environment.Object);
            action.Should().NotThrow<DefaultApplicationException>();
        }
    }
}
