using AutoFixture;
using AutoFixture.AutoMoq;
using Mdm.Core.Models;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Mdm.Core.Tests.Repository
{
    public class FileRepositoryTests
    { 
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Inject(new Config.AppSettings { Database = "testdb.db" });
            Bootstrapper.RegisterCore(new SimpleInjector.Container());
            Bootstrapper.VerifyCore();
        } 
    

        [Test]
        public void When_Upload()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.FileRepository>();
            var input = _fixture.Create<Track>();
            TestDelegate act = new TestDelegate(() => sut.UploadAsync(input).GetAwaiter().GetResult());

            // act

            // assert
            Assert.DoesNotThrow(act);
        }

        [Test]
        public async Task When_List()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.FileRepository>(); 

            // act
            var actual = await sut.ListAsync();
            // assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public async Task When_Download()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.FileRepository>();
            var list = await sut.ListAsync();

            // act
            if (list.Any())
            {
                var actual = await sut.DownloadAsync(list.First());
                // assert
                Assert.IsNotNull(actual);
                Assert.IsNotNull(actual.Content);
                Assert.IsNotEmpty(actual.Content);
            }
            else
            {
                // assert
                Assert.Inconclusive();
            } 
        }

        [Test]
        public async Task When_Delete()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.FileRepository>();
            var list = await sut.ListAsync();

            // act
            if (list.Any())
            {
                var actual = await sut.DeleteAsync(list.First());
                // assert
                Assert.IsTrue(actual);
            }
            else
            {
                // assert
                Assert.Inconclusive();
            }
        }
    }
}
