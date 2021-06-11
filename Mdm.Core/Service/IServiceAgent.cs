using Mdm.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mdm.Core.Service
{
    public interface IServiceAgent
    {
        /// <summary>
        /// Searches file 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QueueSheetItem>> SearchChildrenAsync(string queueSheetId = null, SortDirection sortDirection = SortDirection.Asc, string criteria = null, string[] path = null);


        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="queueFile"></param>
        /// <returns></returns>
        Task<Track> Download(Track queueFile);

        /// <summary>
        /// Searches queue sheetds 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QueueSheet>> SearchQueueSheetsAsync(string searchText = null);

        /// <summary>
        /// Searches queue sheetds 
        /// </summary>
        /// <returns></returns>
        Task<QueueSheet> GetQueueSheetAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueSheet"></param>
        /// <returns></returns>
        Task<QueueSheet> UpdateQueueSheetAsync(QueueSheet queueSheet);

        /// <summary>
        /// Unlocks a queuesheet
        /// </summary>
        /// <param name="queueSheet"></param>
        /// <returns></returns>
        Task<bool> Unlock(QueueSheet queueSheet, int code);

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="userSettings"></param>
        /// <returns></returns>
        Task<bool> Authenticate(UserCredentials userCredentials);

        /// <summary>
        /// Gets the current logged on user information
        /// </summary>
        /// <returns></returns>
        Task<User> GetUserAsync();
         

    }
}
