using Mdm.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mdm.Core.Operations
{
    public interface IRemoteOperations
    {
        /// <summary>
        /// Searches files from the remote (api)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QueueSheetItem>> SearchChildrenAsync(string queueSheetId, SortDirection sortDirection = SortDirection.Asc, string criteria = null, string[] path = null);

        /// <summary>
        /// Downloads a filr from remote (api) and stores it local)
        /// </summary>
        /// <param name="queueFile"></param>
        /// <returns></returns>
        Task<Track> DownloadAsync(Track queueFile, UserSettings userSettings);

        /// <summary>
        /// Searches files from the remote (api)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QueueSheet>> SearchQueueSheetsAsync(string searchText = null); 


        /// <summary>
        /// Unlocks a queuesheet
        /// </summary>
        /// <param name="queueSheet"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> Unlock(QueueSheet queueSheet, int code);

        /// <summary>
        /// Authenticats the current user
        /// </summary>
        /// <param name="userSettings"></param>
        /// <returns></returns>
        Task<bool> Authenticate(UserCredentials userCredentials);

        /// <summary>
        /// Returns the current logged on user
        /// </summary>
        /// <returns></returns>
        Task<User> GetUserAsync();
         
    }
}
