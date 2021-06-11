using AutoFixture;
using AutoFixture.AutoMoq;
using Mdm.Core.Config;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using Mdm.Core.Repository;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mdm.Core.Tests.Operations
{
    public class LocalOperationsTests
    {
        private IFixture _fixture;


        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Inject(new Config.AppSettings { Database = "testdb.db" });
            //Bootstrapper.RegisterCore(new SimpleInjector.Container());
            //Bootstrapper.VerifyCore();
        }

        [Test]
        public async Task When_Get()
        {
            // assign 
            var mockRepository = _fixture.Freeze<Mock<IRepository>>();
            var mockFileRepository = _fixture.Freeze<Mock<IFileRepository>>();
            var sut = _fixture.Create<LocalOperations>();
            var mockData = _fixture.Create<Track>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(mockData)).Verifiable();
            mockFileRepository.Setup(m => m.DownloadAsync(It.IsAny<Track>())).Returns((Track localFile) => Task.FromResult(localFile)).Verifiable();

            // act
            var actual = await sut.GetAsync(1);

            // assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Title, mockData.Title);
            mockRepository.Verify();
            mockFileRepository.Verify();
        }

        [Test]
        public async Task When_Cleanup()
        {
            // assign
            var mockRepository = _fixture.Freeze<Mock<IRepository>>();
            var mockFileRepository = _fixture.Freeze<Mock<IFileRepository>>();
            var sut = _fixture.Create<LocalOperations>();
            var repositoryData = _fixture.CreateMany<Track>();
            var fileData = _fixture.CreateMany<Track>();

            mockRepository.Setup(m => m.SearchExpiredAsync()).Returns(Task.FromResult(repositoryData)).Verifiable();
            mockRepository.Setup(m => m.DeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(true)).Verifiable();
            mockFileRepository.Setup(m => m.ListAsync()).Returns(Task.FromResult(fileData)).Verifiable();
            mockFileRepository.Setup(m => m.DeleteAsync(It.IsAny<Track>())).Returns(Task.FromResult(true)).Verifiable();

            // act
            await sut.CleanupAsync();

            // assert 
            mockRepository.Verify();
            mockFileRepository.Verify();
        }

        [Test]
        public async Task When_Search()
        {
            // assign
            var mockRepository = _fixture.Freeze<Mock<IRepository>>();
            var repositoryData = _fixture.CreateMany<Track>();
            var sut = _fixture.Create<LocalOperations>();
            mockRepository.Setup(m => m.SearchAsync(It.IsAny<string>())).Returns(Task.FromResult(repositoryData)).Verifiable();

            // act
            var actual = await sut.SearchAsync("");

            // assert 
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual);
            mockRepository.Verify();
        }

        [Test]
        public async Task When_GetUserSettings()
        {
            // assign
            var mockRepository = _fixture.Freeze<Mock<IRepository>>();
            var repositoryData = _fixture.Create<UserSettings>();
            var sut = _fixture.Create<LocalOperations>();
            mockRepository.Setup(m => m.GetUserSettingsAsync(It.IsAny<string>())).Returns(Task.FromResult(repositoryData)).Verifiable();

            // act
            var actual = await sut.GetUserSettingsAsync(_fixture.Create<string>());

            // assert 
            Assert.IsNotNull(actual); 
            mockRepository.Verify();
        }

        [Test]
        public async Task When_UpdateUserSettings()
        {
            // assign
            var mockRepository = _fixture.Freeze<Mock<IRepository>>();
            var repositoryData = _fixture.Create<UserSettings>();
            var sut = _fixture.Create<LocalOperations>();
            mockRepository.Setup(m => m.GetUserSettingsAsync(It.IsAny<string>())).Returns(Task.FromResult(repositoryData)).Verifiable();

            // act
            var actual = await sut.GetUserSettingsAsync(_fixture.Create<string>());

            // assert 
            Assert.IsNotNull(actual);
            mockRepository.Verify();
        }
    }
}
