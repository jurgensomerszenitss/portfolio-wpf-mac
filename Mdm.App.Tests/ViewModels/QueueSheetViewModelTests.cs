using AutoFixture;
using AutoFixture.AutoMoq;
using Mdm.App.Infrastructure;
using Mdm.App.ViewModels;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mdm.App.Tests.ViewModels
{
    public class QueueSheetViewModelTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public async Task When_Initialize()
        {
            // assign
            var mockOperations = _fixture.Freeze<Mock<IOperations>>();
            var mockRemoteOperations = _fixture.Freeze<Mock<IRemoteOperations>>();
            var sut = _fixture.Create<QueueSheetViewModel>();

            mockOperations.Setup(x => x.Remote).Returns(mockRemoteOperations.Object);
            mockOperations.Setup(x => x.Authenticate()).Returns(Task.FromResult(true));

            // act
            await sut.Initialize();

            // assert
        }

        [Test]
        public async Task When_SelectCommand()
        {
            // assign
            var actual = _fixture.Create<QueueSheet>();
            var mockStateManager = _fixture.Freeze<Mock<IStateManager>>();
            var sut = _fixture.Create<QueueSheetViewModel>();

            // act
            await sut.SelectCommand.ExecuteAsync(actual);

            // assert
            //mockStateManager.Verify(x => x.QueueSheet);
        }


        [Test]
        public void When_UnlockCommand()
        {
            // assign
            var actual = _fixture.Create<QueueSheet>(); 
            var sut = _fixture.Create<QueueSheetViewModel>();
            var testDelegate = new TestDelegate(() => sut.UnlockCommand.ExecuteAsync(actual).GetAwaiter().GetResult());

            // act

            // assert
            Assert.DoesNotThrow(testDelegate);
        }
    }
}