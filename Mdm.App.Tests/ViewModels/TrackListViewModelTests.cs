using AutoFixture;
using AutoFixture.AutoMoq;
using Mdm.App.Infrastructure;
using Mdm.App.ViewModels;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mdm.App.Tests.ViewModels
{
    public class TrackListViewModelTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public async Task When_OnQueueSheetChanged()
        {
            // assign
            var mockOperations = _fixture.Freeze<Mock<IOperations>>();
            var mockRemoteOperations = _fixture.Freeze<Mock<IRemoteOperations>>();
            var mockStateManager = _fixture.Freeze<Mock<IStateManager>>();

            _fixture.Customize<QueueSheet>(c => c.With(p => p.IsProtected, false));

            var sut = _fixture.Create<TrackListViewModel>();

            mockOperations.Setup(x => x.Remote).Returns(mockRemoteOperations.Object);
            mockRemoteOperations.Setup(x => x.SearchChildrenAsync(It.IsAny<string>(), It.IsAny<SortDirection>(), It.IsAny<string>(), It.IsAny<string[]>())).Returns(Task.FromResult(_fixture.CreateMany<QueueSheetItem>())).Verifiable();
            await sut.Initialize();

            // act
            mockStateManager.Raise(x => x.QueueSheetChanged += null, this, _fixture.Create<QueueSheet>());

            // assert
            Assert.IsNotNull(sut.Children);
            Assert.IsNotEmpty(sut.Children);
            mockRemoteOperations.Verify();
        }

        [Test]
        public async Task When_ToggleIconView()
        {
            // assign
            var sut = _fixture.Create<TrackListViewModel>();

            // act
            await sut.ToggleIconViewCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(ListDisplayMode.Icons, sut.DisplayMode);
        }

        [Test]
        public async Task When_ToggleListView()
        {
            // assign
            var sut = _fixture.Create<TrackListViewModel>();

            // act
            await sut.ToggleListViewCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(ListDisplayMode.List, sut.DisplayMode);
        }

        //[Test]
        //public async Task When_SelectAll()
        //{
        //    // assign
        //    var sut = _fixture.Build<TrackListViewModel>().Without(x => x.SelectedTracks).Create();
        //    sut.Children = new ObservableCollection<QueueSheetItem>(_fixture.CreateMany<QueueSheetItem>(5));

        //    // act
        //    await sut.SelectAllCommand.ExecuteAsync();

        //    // assert
        //    Assert.IsNotNull(sut.SelectedTracks);
        //    Assert.IsNotEmpty(sut.SelectedTracks);
        //    Assert.AreEqual(sut.Children.Count,sut.SelectedTracks.Count);
        //}

        [Test]
        public async Task When_Download()
        {
            // assign
            var mockOperations = _fixture.Freeze<Mock<IOperations>>();
            var mockRemoteOperations = _fixture.Freeze<Mock<IRemoteOperations>>();
            mockOperations.Setup(x => x.Remote).Returns(mockRemoteOperations.Object);
            var sut = _fixture.Create<TrackListViewModel>();
            sut.SelectedTracks = new ObservableCollection<Track>(_fixture.CreateMany<Track>());

            // act
            await sut.DownloadCommand.ExecuteAsync();

            // assert
            mockRemoteOperations.Verify(x => x.DownloadAsync(It.IsAny<Track>(), It.IsAny<UserSettings>()));
        }
    }
}
