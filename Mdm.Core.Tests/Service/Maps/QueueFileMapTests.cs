using AutoFixture;
using AutoFixture.AutoMoq;
using Mapster;
using Mdm.Core.Models;
using Mdm.Core.Service.Contracts;
using NUnit.Framework;

namespace Mdm.Core.Tests.Service.Maps
{
    public class QueueFileMapTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            Bootstrapper.RegisterCore(new SimpleInjector.Container());
            Bootstrapper.VerifyCore();
        }


        [Test]
        public void When_Map_QueueFileContract_To_QueueFile()
        {
            // assign
            var sut = _fixture.Create<TrackContract>();

            // act
            var actual = sut.Adapt<Track>();


            // assert
            Assert.AreEqual(sut.Id, actual.QueueItemId);
            Assert.AreEqual(sut.Title, actual.Title);
            Assert.AreEqual(sut.Source, actual.Source); 
        }

    }
}
