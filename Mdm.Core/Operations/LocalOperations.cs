using Mdm.Core.Models;
using Mdm.Core.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Mdm.Core.Operations
{
    internal class LocalOperations : ILocalOperations
    {
        public LocalOperations(IRepository repository, IFileRepository fileRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
        }

        private readonly IRepository _repository;
        private readonly IFileRepository _fileRepository; 

        public async Task<IEnumerable<Track>> SearchAsync(string queueSheetId)
        {
            return await _repository.SearchAsync(queueSheetId); 
        }

        public async Task<Track> GetAsync(int id)
        {
            var item = await _repository.GetAsync(id);
            await _fileRepository.DownloadAsync(item);
            return item;
        }

        public async Task CleanupAsync()
        {
            await CleanupExpiredAsync();
            await CleanupFileRepositoryAsync();
            await CleanupDataRepositoryAsync();
        }


        public async Task<UserSettings> GetUserSettingsAsync(string userName)
        {
            return await _repository.GetUserSettingsAsync(userName);
        } 

        public async Task<UserSettings> UpdateUserSettings(UserSettings settings)
        {
            return await _repository.UpdateUserSettingsAsync(settings);
        }

        public async Task<UserCredentials> GetUserCredentialsAsync()
        {
            return await _repository.GetUserCredentialsAsync();
        }

        public async Task<UserCredentials> SetUserCredentialsAsync(UserCredentials userCredentials)
        {
            return await _repository.SetUserCredentialsAsync(userCredentials);
        }

        public async Task<string> GetForgotPasswordUrlAsync()
        {
            return await _repository.GetForgotPasswordUrlAsync();
        }


        private async Task CleanupExpiredAsync()
        {
            // get the local expired files
            var files = await _repository.SearchExpiredAsync();

            // remove the expired files
            foreach (var file in files)
            {
                await _fileRepository.DeleteAsync(file);
                await _repository.DeleteAsync(file.Id);
            }
        }
        private async Task CleanupFileRepositoryAsync()
        {
            var allFiles = await _fileRepository.ListAsync();
            var repositoryFiles = await _repository.SearchAsync();

            var missing = allFiles.Except(repositoryFiles, new LocalFileComparer()).ToList();
            foreach (var file in missing)
            {
                await _fileRepository.DeleteAsync(file);
            }
        }

        private async Task CleanupDataRepositoryAsync()
        {
            var allFiles = await _fileRepository.ListAsync();
            var repositoryFiles = await _repository.SearchAsync();

            var missing = repositoryFiles.Except(allFiles, new LocalFileComparer()).ToList();
            foreach (var file in missing)
            {
                await _repository.DeleteAsync(file.Id);
            }
        }

        private class LocalFileComparer : IEqualityComparer<Track>
        {
            public bool Equals(Track x, Track y)
            {
                return x != null && y != null && x.Id == y.Id && x.Id != 0 && y.Id != 0;
            }

            public int GetHashCode(Track obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
