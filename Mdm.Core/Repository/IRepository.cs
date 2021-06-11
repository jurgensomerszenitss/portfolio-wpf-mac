using Mdm.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mdm.Core.Repository
{
    public interface IRepository
    {
        Task<Track> CreateAsync(Track localFile); 
        Task<bool> DeleteAsync(int id); 
        Task<Track> GetAsync(int id);
        Task<IEnumerable<Track>> SearchAsync(string queueSheetId = null);
        Task<IEnumerable<Track>> SearchExpiredAsync();


        Task<UserSettings> GetUserSettingsAsync(string userName);
        Task<UserSettings> UpdateUserSettingsAsync(UserSettings settings);
        Task<UserCredentials> SetUserCredentialsAsync(UserCredentials userCredentials);
        Task<UserCredentials> GetUserCredentialsAsync();
        Task<string> GetForgotPasswordUrlAsync();
    }
}
