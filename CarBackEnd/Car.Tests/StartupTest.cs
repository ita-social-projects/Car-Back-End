using System;
using System.Data;
using Car.BLL.Exceptions;
using CarBackEnd;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Car.Tests
{
    public class StartupTest
    {
        private Startup _startup;
        private IServiceCollection _serviceCollection;
        private IApplicationBuilder _applicationBuilder;
        private IWebHostEnvironment _environment;


        [Fact]
        public void TestConfigureServices()
        {
            Action action = () => _startup.ConfigureServices(_serviceCollection);
            action.Should().NotThrow<DefaultApplicationException>();
        }

        [Fact]
        public void TestConfigure()
        {
            Action action = () => _startup.Configure(_applicationBuilder, _environment);
            action.Should().NotThrow<DefaultApplicationException>();
        }
    }
}
