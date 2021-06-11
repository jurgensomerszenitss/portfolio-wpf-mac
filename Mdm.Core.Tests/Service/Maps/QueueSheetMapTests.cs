using AutoFixture;
using AutoFixture.AutoMoq;
using Mapster;
using Mdm.Core.Models;
using Mdm.Core.Service.Contracts;
using NUnit.Framework;

namespace Mdm.Core.Tests.Service.Maps
{
    public class QueueSheetMapTests
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
        public void When_Map_QueueSheetContract_To_QueueSheet()
        {
            // assign
            var sut = _fixture.Create<QueueSheetContract>();

            // act
            var actual = sut.Adapt<QueueSheet>();


            // assert
            Assert.AreEqual(sut.Id, actual.Id);
            Assert.AreEqual(sut.Name, actual.Name);
            Assert.AreEqual(sut.IsProtected, actual.IsProtected);
        }

    }
}
