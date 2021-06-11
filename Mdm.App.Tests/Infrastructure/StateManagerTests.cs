using AutoFixture;
using AutoFixture.AutoMoq;
using Mdm.App.Infrastructure;
using Mdm.App.ViewModels;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Mdm.App.Tests.Infrastructure
{
    public class StateManagerTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public void When_SetSelectedQueueSheet()
        {
            // assign 
            var sut = _fixture.Create<StateManager>();
            var item = _fixture.Create<QueueSheet>();
            bool eventWasRaised = false;
            sut.QueueSheetChanged += (s, o) =>
            {
                eventWasRaised = true;
            };

            // act
            sut.QueueSheet = item;

            // assert
            Assert.IsNotNull(sut.QueueSheet);
            Assert.IsTrue(eventWasRaised);
        }
    }
}
