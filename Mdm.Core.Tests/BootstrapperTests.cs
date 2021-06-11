using Mdm.Core.Config;
using NUnit.Framework;
using SimpleInjector;
using System;

namespace Mdm.Core.Tests
{
    public class BootstrapperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void When_Boostrapper_Configure()
        {
            // assign
            TestDelegate configureDelegate = () => Bootstrapper.ConfigureCore(new Container());

            // act

            // assert
            Assert.DoesNotThrow(configureDelegate);
        }

        [Test]
        public void When_Boostrapper_RegisterCore()
        {
            // assign
            var sut = new Container();
            sut.RegisterInstance(new AppSettings()); // --> settings are set in container during configuration

            Bootstrapper.RegisterCore(sut);
            TestDelegate configureDelegate = sut.Verify;

            // act

            // assert
            Assert.DoesNotThrow(configureDelegate);
        }
    }
}
