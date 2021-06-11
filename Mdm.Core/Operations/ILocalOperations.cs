using Mdm.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mdm.Core.Operations
{
    public interface ILocalOperations
    {
        /// <summary>
        /// Searches files in the local file repository
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Track>> SearchAsync(string queueSheetId);

        /// <summary>
        /// Cleans the local file repository
        /// </summary>
        /// <returns></returns>
        Task CleanupAsync();

        /// <summary>
        /// Gets an item (with content) from the local file repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Track> GetAsync(int id);

        Task<UserSettings> GetUserSettingsAsync(string userName);
        Task<UserSettings> UpdateUserSettings(UserSettings settings);

        Task<UserCredentials> GetUserCredentialsAsync();
        Task<UserCredentials> SetUserCredentialsAsync(UserCredentials userCredentials);
        Task<string> GetForgotPasswordUrlAsync();
    }
}
