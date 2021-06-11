using Mdm.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mdm.Core.Repository
{
    public interface IFileRepository
    {
        Task<bool> UploadAsync(Track localFile);
        Task<Track> DownloadAsync(Track localFile);
        Task<IEnumerable<Track>> ListAsync();
        Task<bool> DeleteAsync(Track localFile);
    }
}
