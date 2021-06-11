using Mdm.Core.Extensions;
using System;

namespace Mdm.Core.Models
{
    public class Track : QueueSheetItem
    {
        public int Id { get; set; } 
        public string Source { get; set; }
        public string Path { get; set; }
        public string Artist { get; set; }  
        public DateTime DownloadedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public byte[] Content { get; set; }
        public string CoverUrl { get; set; }

        public string FullPath => $"{System.IO.Path.Combine(Path, $"{Artist} - {Title} - {DownloadedAt.ToTag()}")}.mp3";
    }
}
