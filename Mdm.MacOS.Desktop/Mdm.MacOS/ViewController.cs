using System;
using CoreGraphics;
using AppKit;
using Foundation;
using Mdm.MacOS.Assets;
using Mdm.App;
using Mdm.App.ViewModels;
using Mdm.MacOS.Infrastructure;
using Mdm.MacOS.QueueSheetLists;

namespace Mdm.MacOS
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public QueueSheetListDataSource QueueSheetListDatasource { get; set; }
        private IQueueSheetViewModel _queueSheetViewModel;
        private QueueSheetModel _queueSheetSelected;

        [Export("QueueSheetSelected")] 
        public QueueSheetModel QueueSheetSelected
        {
            get { return _queueSheetSelected; }
            set
            {
                WillChangeValue("QueueSheetSelected");
                _queueSheetSelected = value;
                DidChangeValue("QueueSheetSelected");
                RaiseSelectionChanged();
            }
        }

        public NSView Content
        {
            get { return ContentView; }
        }


        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            _queueSheetViewModel = MainClass.Container.GetInstance<IQueueSheetViewModel>();

            UserName.StringValue = "";
            ConfigureQueueSheetListView();

            _queueSheetViewModel.OnShowLogin += _queueSheetViewModel_OnShowLogin;
            _queueSheetViewModel.OnUnlock += _queueSheetViewModel_OnUnlock;
            _queueSheetViewModel.PropertyChanged += _queueSheetViewModel_PropertyChanged;
            await _queueSheetViewModel.Initialize();

            this.View.Layer.BackgroundColor = Colors.ColorMenuBackground.CGColor;

            _fldSearch.Layer.BorderColor = Colors.ColorQueueSheetBorder.CGColor;
            _fldSearch.Layer.BorderWidth = 1;
            _fldSearch.Layer.CornerRadius = 5;

            _btnSearch.Layer.BorderWidth = 1;
            _btnSearch.Layer.BorderColor = Colors.ColorQueueSheetBorder.CGColor;
            _btnSearch.Layer.CornerRadius = 5;

            _divider1.BorderColor = Colors.ColorSeparator;
            _divider1.BorderType = NSBorderType.LineBorder;
            _divider1.BoxType = NSBoxType.NSBoxCustom;
            _divider1.SetFrameSize(new CoreGraphics.CGSize(320, 1));

            BeginInvokeOnMainThread(() => this.PerformSegue("TracksViewSegue", this.View));
            QueueView.AutoresizingMask = NSViewResizingMask.HeightSizable;
        } 

        private void ConfigureQueueSheetListView()
        {
            QueueSheetCollection.RegisterClassForItem(typeof(QueueSheetListItemController), "QueueSheetCell");
            QueueSheetCollection.WantsLayer = true;
            QueueSheetCollection.Delegate = new QueueSheetListDelegate(this);

        }

        private void _queueSheetViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(IQueueSheetViewModel.UserName): BeginInvokeOnMainThread(() => UserName.StringValue = _queueSheetViewModel.UserName); break;
                case nameof(IQueueSheetViewModel.List): BeginInvokeOnMainThread(() => SetQueueSheetList()); break;
            } 
        }

        partial void SearchClicked(NSObject sender)
        {
            _queueSheetViewModel.SearchText = _fldSearch.StringValue;
            _queueSheetViewModel.SearchCommand.Execute(null);
        }

        partial void RefreshClicked(NSObject sender)
        {
            _queueSheetViewModel.RefreshCommand.Execute(null);
        }

        private void SetQueueSheetList()
        {
            var list = _queueSheetViewModel.List;
            if (QueueSheetListDatasource == null) QueueSheetListDatasource = new QueueSheetListDataSource(QueueSheetCollection);
            QueueSheetListDatasource.Data.Clear();
            foreach (var item in list)
            {
                var queueSheetModel = new QueueSheetModel
                {
                    Name = item.Name,
                    Id = item.Id,
                    IsProtected = item.IsProtected,
                    IsUnlocked = item.IsUnlocked,
                    Origin = item
                };
                QueueSheetListDatasource.Data.Add(queueSheetModel);
            } 

            QueueSheetCollection.ReloadData(); 
        }

        private void _queueSheetViewModel_OnShowLogin(object sender, EventArgs e)
        {
            BeginInvokeOnMainThread(() => this.PerformSegue("LoginSegue", this.View));
        }

        private void _queueSheetViewModel_OnUnlock(object sender, Core.Models.QueueSheet e)
        {
            BeginInvokeOnMainThread(() => this.PerformSegue("UnlockSegue", this.View));
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        } 

        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            
            // Take action based on the segue name
            switch (segue.Identifier)
            {
                //case "UnlockSegue":
                //    var dialog = segue.DestinationController as UnlockController;                
                //    dialog.DialogAccepted += (s, e) => {
                //        Console.WriteLine("Dialog accepted");
                //    };
                //    dialog.Presentor = this;
                //    break;
                case "LoginSegue":
                    var login = segue.DestinationController as LoginViewController; 
                    login.Presentor = this;
                    break;
                case "UserSettingsSegue":
                    var userSettings = segue.DestinationController as UserSettingsViewController;
                    userSettings.Presentor = this;
                    break;
                case "UnlockSegue":
                    var unlock = segue.DestinationController as UnlockViewController;
                    unlock.Presentor = this;
                    unlock.Accepted += (s, e) =>
                    {
                        RaiseSelectionChanged();
                    };
                    break;
            }
        }

        public delegate void SelectionChangedDelegate();
         
		public event SelectionChangedDelegate SelectionChanged;
         
        internal void RaiseSelectionChanged()
        {
            if (_queueSheetSelected == null)
                _queueSheetViewModel.SelectCommand.Execute(null);
            else
                _queueSheetViewModel.SelectCommand.Execute(_queueSheetSelected.Origin);
            // Inform caller
            if (this.SelectionChanged != null) SelectionChanged();
        }
    }
}
