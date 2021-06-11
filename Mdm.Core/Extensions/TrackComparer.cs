using Mdm.Core.Models;
using System.Collections.Generic;

namespace Mdm.Core.Extensions
{
    public class TrackComparer : IEqualityComparer<Track>
    {
        public bool Equals(Track x, Track y)
        {
            return x != null && y != null && x.QueueSheetId == y.QueueSheetId && x.Title == y.Title;
        }

        public int GetHashCode(Track obj)
        {
            return obj.QueueSheetId.GetHashCode() ^ (obj.Title ?? "").ToLower().GetHashCode();
        }
    }
}
