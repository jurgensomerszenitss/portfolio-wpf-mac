using AutoFixture;
using AutoFixture.AutoMoq;
using Mdm.Core.Config;
using Mdm.Core.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mdm.Core.Tests.Repository
{
    public class RepositoryTests
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
        public async Task When_Create()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.Repository>();
            var input = _fixture.Create<Track>();

            // act
            var actual = await sut.CreateAsync(input);

            // assert
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(actual.Id, 0);
            Assert.AreEqual(actual.DownloadedAt, input.DownloadedAt);
            Assert.AreEqual(actual.ExpiresAt, input.ExpiresAt);
            Assert.AreEqual(actual.Title, input.Title);
            Assert.AreEqual(actual.Path, input.Path);
            Assert.AreEqual(actual.Source, input.Source);
        }


        [Test]
        public async Task When_Search()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.Repository>();

            // act
            var actual = await sut.SearchAsync();

            // assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public async Task When_SearchExpired()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.Repository>();

            // act
            var actual = await sut.SearchExpiredAsync();

            // assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public async Task When_Get()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.Repository>();
            var firstItem = sut.SearchAsync("").GetAwaiter().GetResult().FirstOrDefault();

            // act
            if(firstItem == null)
            {
                Assert.Inconclusive("No items available");
            }
            else
            {
                var actual = await sut.GetAsync(firstItem.Id);

                // assert
                Assert.IsNotNull(actual);
            } 
        }

        [Test]
        public async Task When_Delete()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.Repository>();
            var firstItem = sut.SearchAsync("").GetAwaiter().GetResult().FirstOrDefault();

            // act
            if (firstItem == null)
            {
                Assert.Inconclusive("No items available");
            }
            else
            {
                var actual = await sut.DeleteAsync(firstItem.Id);

                // assert
                Assert.IsTrue(actual);
            }
        }

        [Test]
        public async Task When_GetUserSettings()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.Repository>();


            // act
            var actual = await sut.GetUserSettingsAsync(_fixture.Create<string>());
            // assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public async Task When_UpdateUserSettings()
        {
            // assign
            var sut = _fixture.Create<Mdm.Core.Repository.Repository>();
            var item = _fixture.Create<UserSettings>();


            // act
            var actual = await sut.UpdateUserSettingsAsync(item);

            // assert
            Assert.IsNotNull(actual);
        }
    }
}
