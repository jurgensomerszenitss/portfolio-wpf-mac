using AutoFixture;
using AutoFixture.AutoMoq;
using Mdm.Core.Config;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using Mdm.Core.Repository;
using Mdm.Core.Service;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mdm.Core.Tests.Operations
{
    public class RemoteOperationsTests
    {
        private IFixture _fixture;


        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Inject(new Config.AppSettings { Database = "testdb.db" });
        }

        [Test]
        public async Task When_Download()
        {
            // assign
            var mockServiceAgent = _fixture.Freeze<Mock<IServiceAgent>>();
            var mockRepository = _fixture.Freeze<Mock<IRepository>>();
            var mockFileRepository = _fixture.Freeze<Mock<IFileRepository>>();
            var input = _fixture.Create<Track>();
            var sut = _fixture.Create<RemoteOperations>();

            mockServiceAgent.Setup(m => m.Download(It.IsAny<Track>())).Returns(Task.FromResult(input)).Verifiable();
            mockRepository.Setup(m => m.CreateAsync(It.IsAny<Track>())).Returns((Track lf) => Task.FromResult(lf)).Verifiable();
            mockFileRepository.Setup(m => m.UploadAsync(It.IsAny<Track>())).Returns(Task.FromResult(true)).Verifiable();
            mockRepository.Setup(m => m.GetUserSettingsAsync(It.IsAny<string>())).Returns(Task.FromResult(_fixture.Create<UserSettings>()));

            // act
            var actual = await sut.DownloadAsync(input, new UserSettings { FileLocation = "a" });

            // assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Title, input.Title);
            mockServiceAgent.Verify();
            mockRepository.Verify();
            mockFileRepository.Verify();
        }

        [Test]
        public async Task When_Search()
        {
            // assign
            var mockServiceAgent = _fixture.Freeze<Mock<IServiceAgent>>(); 
            var input = _fixture.Create<Track>();
            var sut = _fixture.Create<RemoteOperations>();
            mockServiceAgent.Setup(m => m.GetQueueSheetAsync(It.IsAny<string>())).Returns(Task.FromResult(_fixture.Build<QueueSheet>().With(p => p.IsProtected, false).Create())).Verifiable();
            mockServiceAgent.Setup(m => m.SearchChildrenAsync(It.IsAny<string>(), It.IsAny<SortDirection>(), It.IsAny<string>(), It.IsAny<string[]>())).Returns(Task.FromResult(_fixture.CreateMany<QueueSheetItem>())).Verifiable();
          
            // act
            var actual = await sut.SearchChildrenAsync(string.Empty, SortDirection.Asc,null,null);

            // assert
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual);
            mockServiceAgent.Verify();
        }

        [Test]
        public void When_Unlock()
        {
            // assign  
            var sut = _fixture.Create<RemoteOperations>();

            TestDelegate act = new TestDelegate(() => sut.Unlock(_fixture.Create<QueueSheet>(), _fixture.Create<int>()).GetAwaiter().GetResult());

            // act

            // assert
            Assert.DoesNotThrow(act);  
        }

    }
}
