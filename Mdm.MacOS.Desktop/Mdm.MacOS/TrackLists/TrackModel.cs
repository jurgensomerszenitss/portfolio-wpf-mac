using System;
using Foundation;
using Mdm.Core.Models;

namespace Mdm.MacOS.TrackLists
{
    [Register("TrackModel")]
    public partial class TrackModel : NSObject
    {
        public TrackModel()
        {
        }
        private string _id;
        private string _title;
        private string _coverUrl;
        private bool _isFolder;
        private QueueSheetItem _origin;

        [Export("Id")]
        public string Id
        {
            get => _id;
            set
            {
                WillChangeValue("Id");
                _id = value;
                DidChangeValue("Id");
            }
        }

        [Export("Title")]
        public string Title
        {
            get => _title;
            set
            {
                WillChangeValue("Title");
                _title = value;
                DidChangeValue("Title");
            }
        }

        [Export("CoverUrl")]
        public string CoverUrl
        {
            get => _coverUrl;
            set
            {
                WillChangeValue("CoverUrl");
                _coverUrl = value;
                DidChangeValue("CoverUrl");
            }
        }

        [Export("IsFolder")]
        public bool IsFolder
        {
            get => _isFolder;
            set
            {
                WillChangeValue("IsFolder");
                _isFolder = value;
                DidChangeValue("IsFolder");
            }
        }

        public QueueSheetItem Origin
        {
            get => _origin;
            set => _origin = value;
        }

    }
}
