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
    public class UserSettingsViewModelTests
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
            var mockLocalOperations = _fixture.Freeze<Mock<ILocalOperations>>();
            var mockRemoteOperations = _fixture.Freeze<Mock<IRemoteOperations>>();          

            var mockStateManager = _fixture.Freeze<Mock<IStateManager>>();

            var sut = _fixture.Create<UserSettingsViewModel>();
            var userSettings = _fixture.Create<UserSettings>();
            var user = _fixture.Create<User>();

            mockOperations.Setup(x => x.Local).Returns(mockLocalOperations.Object);
            mockOperations.Setup(x => x.Remote).Returns(mockRemoteOperations.Object);
            mockStateManager.Setup(x => x.UserSettings).Returns(userSettings);
            mockRemoteOperations.Setup(x => x.GetUserAsync()).Returns(Task.FromResult(user));

            // act
            await sut.Initialize();

            // assert
            Assert.AreEqual(userSettings.UserName, user.UserName ); 
            Assert.AreEqual(userSettings.FileLocation, sut.FileLocation);
            Assert.AreEqual(userSettings.SyncInterval, sut.SyncInterval);
            mockLocalOperations.Verify();
        }

        [Test]
        public async Task When_Save()
        {
            // assign
            var mockOperations = _fixture.Freeze<Mock<IOperations>>();
            var mockLocalOperations = _fixture.Freeze<Mock<ILocalOperations>>();
            mockOperations.Setup(x => x.Local).Returns(mockLocalOperations.Object);
            
            var sut = _fixture.Create<UserSettingsViewModel>(); 

            // act
            await sut.SaveCommand.ExecuteAsync();

            // assert
            mockLocalOperations.Verify(x => x.UpdateUserSettings(It.IsAny<UserSettings>()));
        } 

        [Test]
        public void When_Invalid_FileLocation()
        {
            // assign
            var sut = _fixture.Build<UserSettingsViewModel>().Without(p => p.FileLocation).Create();

            // act
            var actual = sut[nameof(UserSettingsViewModel.FileLocation)];

            // assert
            Assert.IsFalse(sut.IsValid);
            Assert.AreSame("File location is mandatory", actual);
        }
    }
}
