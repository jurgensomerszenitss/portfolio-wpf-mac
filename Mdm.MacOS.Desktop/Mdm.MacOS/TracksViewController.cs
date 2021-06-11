// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using Mdm.App.ViewModels;
using Mdm.MacOS.Assets;
using Mdm.MacOS.TrackLists;
using Mdm.Core.Models;
using System.Linq;

namespace Mdm.MacOS
{
	public partial class TracksViewController : NSViewController
	{
		public TracksViewController (IntPtr handle) : base (handle)
		{
		}

		private ITrackListViewModel _trackListViewModel;
        private TrackModel _trackSelected;
        public TrackListDataSource TrackListDataSource { get; set; }

        [Export("TrackSelected")]
        public TrackModel TrackSelected
        {
            get { return _trackSelected; }
            set
            {
                WillChangeValue("TrackSelected");
                _trackSelected = value;
                DidChangeValue("TrackSelected");
                RaiseSelectionChanged();
            }
        }

        public void SelectTrack(TrackModel trackModel)
        {
            if (!trackModel.IsFolder)
            {
                _trackListViewModel.SelectedTracks.Add(trackModel.Origin as Track);
                RaiseSelectionChanged();
            }
            else
            {
                _trackListViewModel.GoToFolderCommand.Execute(trackModel.Title);
            }
        }

        public void UnselectTrack(TrackModel trackModel)
        {
            if (!trackModel.IsFolder)
            {
                _trackListViewModel.SelectedTracks.Remove(trackModel.Origin as Track);
                RaiseSelectionChanged();
            }
        }

        public void UnselectAllTracks()
        {
            _trackListViewModel.SelectedTracks.Clear();
            RaiseSelectionChanged();
        }


        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            _trackListViewModel = MainClass.Container.GetInstance<ITrackListViewModel>();
            _trackListViewModel.PropertyChanged += _trackListViewModel_PropertyChanged;
            ConfigureTrackListView();
            await _trackListViewModel.Initialize();

            _divider1.BorderColor = Colors.ColorSeparator;
            _divider1.BorderType = NSBorderType.LineBorder;
            _divider1.BoxType = NSBoxType.NSBoxCustom;
            _divider1.SetFrameSize(new CoreGraphics.CGSize(640, 1));

           
            SetActivateButtonState(false);

            btnIconView.WantsLayer = true;
            btnIconView.Layer.CornerRadius = 4;
            btnIconView.Layer.BorderWidth = 1;
            btnIconView.Layer.BorderColor = Colors.ColorTrackViewButton.CGColor;

            btnTableView.WantsLayer = true;
            btnTableView.Layer.CornerRadius = 4;
            btnTableView.Layer.BorderWidth = 1;
            btnTableView.Layer.BorderColor = Colors.ColorTrackViewButton.CGColor;

            btnSort.WantsLayer = true;
            btnSort.Layer.CornerRadius = 4;
            btnSort.Layer.BorderWidth = 1;
            btnSort.Layer.BorderColor = Colors.ColorTrackViewButton.CGColor;

            _fldSearch.Layer.BorderColor = Colors.ColorQueueSheetBorder.CGColor;
            _fldSearch.Layer.BorderWidth = 1;
            _fldSearch.Layer.CornerRadius = 5;

            _btnSearch.Layer.BorderWidth = 1;
            _btnSearch.Layer.BorderColor = Colors.ColorQueueSheetBorder.CGColor;
            _btnSearch.Layer.CornerRadius = 5;

            TrackTable.Layer.BorderColor = NSColor.Clear.CGColor;

            SetIconView();

        }

        private void SetIconView()
        {
            btnIconView.Image = NSImage.ImageNamed("IconIconViewActive");
            btnIconView.Layer.BorderColor = Colors.ColorTrackViewButtonActive.CGColor;

            btnTableView.Image = NSImage.ImageNamed("IconTableView");
            btnTableView.Layer.BorderColor = Colors.ColorTrackViewButton.CGColor;

            IconContainer.Hidden = false;
            TableContainer.Hidden = true;

            SetTrackList();
        }

        private void SetTableView()
        {
            btnIconView.Image = NSImage.ImageNamed("IconIconView");
            btnTableView.Layer.BorderColor = Colors.ColorTrackViewButtonActive.CGColor;

            btnTableView.Image = NSImage.ImageNamed("IconTableViewActive");
            btnIconView.Layer.BorderColor = Colors.ColorTrackViewButton.CGColor;

            IconContainer.Hidden = true;
            TableContainer.Hidden = false;

            SetTrackTable();
        }


        partial void IconViewClicked(NSObject sender)
        {
            SetIconView();
        }

        partial void TableViewClicked(NSObject sender)
        {
            SetTableView();
        }

        partial void SortClicked(NSObject sender)
        {
            _trackListViewModel.OrderCommand.Execute(null);
        }

        private void SetActivateButtonState(bool enabled)
        {
            _btnActivate.Enabled = enabled;

            var color = enabled ? Colors.ColorTracksButtonActivateEnabled : Colors.ColorTracksButtonActivate;

            _btnActivate.WantsLayer = true;
            _btnActivate.Layer.CornerRadius = 20;
            _btnActivate.Layer.BorderWidth = 1;

            _btnActivate.Enabled = true;
            _btnActivate.Layer.BorderColor = color.CGColor;
            _btnActivate.ContentTintColor = color;

           
            if (enabled)
                _btnActivate.Image = NSImage.ImageNamed("IconDownloadActive");
            else
                _btnActivate.Image = NSImage.ImageNamed("IconDownload");
        }

     

        partial void ActivateClicked(NSObject sender)
        {
            if (_trackListViewModel.DownloadCommand.CanExecute(null))
                _trackListViewModel.DownloadCommand.Execute(null);
        }

        private void _trackListViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName) 
            {
                case nameof(ITrackListViewModel.Children): BeginInvokeOnMainThread(() => { SetTrackList(); SetTrackTable(); }); break;
                case nameof(ITrackListViewModel.FullPath): BeginInvokeOnMainThread(() => _fldPath.StringValue = _trackListViewModel.FullPath); break;
                case nameof(ITrackListViewModel.SelectedTracks): BeginInvokeOnMainThread(() => SetActivateButtonState(_trackListViewModel.SelectedTracks.Any())); break;
            }
        }

        private void ConfigureTrackListView()
        {
            TrackCollection.RegisterClassForItem(typeof(TrackListItemController), "TrackCell");
            TrackCollection.WantsLayer = true;
            TrackCollection.AllowsMultipleSelection = true;

            // Setup collection view
            TrackCollection.Delegate = new TrackListDelegate(this);
            TrackCollection.Layer.BackgroundColor = Colors.ColoryTrackListBackground.CGColor; 
        }
 

        public delegate void SelectionChangedDelegate();

        public event SelectionChangedDelegate SelectionChanged;

        internal void RaiseSelectionChanged()
        { 
            // Inform caller
            if (this.SelectionChanged != null) SelectionChanged();
            _fldCount.StringValue = $"{_trackListViewModel.SelectedTracks.Count} / {_trackListViewModel.Children.OfType<Track>().Count()}";

            BeginInvokeOnMainThread(() => SetActivateButtonState(_trackListViewModel.SelectedTracks.Any()));
        }

        private void SetTrackList()
        {
            if (_trackListViewModel.Children == null) return;

            var list = _trackListViewModel.Children;
            TrackListDataSource = new TrackListDataSource(TrackCollection);
            _trackListViewModel.SelectedTracks?.Clear();

            foreach (var item in list)
            {
                var isTrack = item is Track;

                var trackModel = new TrackModel
                {
                    Title = item.Title,
                    Id = item.QueueItemId,
                    Origin = item,
                    CoverUrl = isTrack ? ((Track)item).CoverUrl : null,
                    IsFolder = item is Folder
                };
                TrackListDataSource.Data.Add(trackModel);
            }

            TrackCollection.ReloadData();
            _fldCount.StringValue = $"0 / {list.OfType<Track>().Count()}";
        }

        private void SetTrackTable()
        {
            if (_trackListViewModel.Children == null) return;

            var dataSource = new TrackTableDataSource();
            var list = _trackListViewModel.Children;
            _trackListViewModel.SelectedTracks?.Clear();

            foreach (var item in list)
            {
                var isTrack = item is Track;

                var trackModel = new TrackModel
                {
                    Title = item.Title,
                    Id = item.QueueItemId,
                    Origin = item,
                    CoverUrl = isTrack ? ((Track)item).CoverUrl : null,
                    IsFolder = item is Folder
                };
                dataSource.Tracks.Add(trackModel);
            }

            TrackTable.Delegate = new TrackTableDelegate(dataSource, this);
            TrackTable.DataSource = dataSource;
        }

        partial void SearchClicked(NSObject sender)
        {
            _trackListViewModel.SearchText = _fldSearch.StringValue;
            _trackListViewModel.SearchCommand.Execute(null);
        }

        partial void SearchTextChanged(NSObject sender)
        {
            _trackListViewModel.SearchText = _fldSearch.StringValue;
        }

    }
}