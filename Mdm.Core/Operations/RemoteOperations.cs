using Mdm.Core.Models;
using Mdm.Core.Repository;
using Mdm.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mdm.Core.Operations
{
    internal class RemoteOperations : IRemoteOperations
    {
        public RemoteOperations(IServiceAgent serviceAgent, Config.AppSettings settings, IRepository repository, IFileRepository fileRepository)
        {
            _serviceAgent = serviceAgent;
            _settings = settings;
            _repository = repository;
            _fileRepository = fileRepository;
            _queueSheetCodes = new Dictionary<string, int>();
        }

        private readonly IServiceAgent _serviceAgent;
        private readonly IRepository _repository;
        private readonly IFileRepository _fileRepository;
        private readonly Config.AppSettings _settings;
        private readonly IDictionary<string, int> _queueSheetCodes;

        public async Task<IEnumerable<QueueSheetItem>> SearchChildrenAsync(string queueSheetId, SortDirection sortDirection, string criteria, string[] path)
        {
            var queueSheet = await _serviceAgent.GetQueueSheetAsync(queueSheetId);
            if (queueSheet != null)
            {
                if (queueSheet.IsProtected && _queueSheetCodes.ContainsKey(queueSheetId) || !queueSheet.IsProtected)
                {
                    return await _serviceAgent.SearchChildrenAsync(queueSheetId, sortDirection, criteria, path);
                }
            }
            return new List<Track>();
        }

        public async Task<Track> DownloadAsync(Track queueFile, UserSettings userSettings)
        {
            // download file from remote
            var sourceFile = await _serviceAgent.Download(queueFile); 

            if (userSettings.IsValid && sourceFile != null)
            {

                // convert file to local file
                var targetFile = new Track
                {
                    Id = queueFile.Id,
                    QueueSheetId = queueFile.QueueSheetId,
                    DownloadedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddDays(_settings.Expires),
                    Title = sourceFile.Title,
                    Artist = sourceFile.Artist,
                    Content = sourceFile.Content,
                    Path = userSettings.FileLocation, 
                    CoverUrl = sourceFile.CoverUrl,
                    Source = sourceFile.Source
                };

                // save target file on local system
                if (await _fileRepository.UploadAsync(targetFile))
                {
                    // save the data in the local repository
                    await _repository.CreateAsync(targetFile);
                }

                return targetFile;
            }
            return null;
        }

        public async Task<IEnumerable<QueueSheet>> SearchQueueSheetsAsync(string searchText = null)
        {
            var queueSheets =  await _serviceAgent.SearchQueueSheetsAsync(searchText);
            queueSheets = queueSheets.Select(x => { x.IsUnlocked = _queueSheetCodes.ContainsKey(x.Id); return x; }).ToList();
            return queueSheets;
        }

       

        public async Task<bool> Unlock(QueueSheet queueSheet, int code)
        {
            try
            {
                var success = await _serviceAgent.Unlock(queueSheet, code);
                if (success)
                {
                    if (_queueSheetCodes.ContainsKey(queueSheet.Id))
                        _queueSheetCodes[queueSheet.Id] = code;
                    else
                        _queueSheetCodes.Add(queueSheet.Id, code);
                    queueSheet.IsUnlocked = true;
                }

                return success;
            }
            catch
            {
                return false;
            }
          
        }

        public async Task<bool> Authenticate(UserCredentials userCredentials)
        {
            return await _serviceAgent.Authenticate(userCredentials);
        }

        public async Task<User> GetUserAsync()
        {
            return await _serviceAgent.GetUserAsync();
        }
         
    }
}
