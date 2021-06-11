using LiteDB;
using Mdm.Core.Config;
using Mdm.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mdm.Core.Repository
{ 

    internal class FileRepository : IFileRepository
    {
        public FileRepository(Config.AppSettings settings)
        {
            _connectionString = $"Filename={settings.LocalFolder}{settings.Database}";
            _settings = settings;
        }

        private readonly string _connectionString;
        private readonly Config.AppSettings _settings;

        public async Task<bool> UploadAsync(Track localFile)
        {
            try
            {
                return await Task.FromResult(SaveToDisk(localFile));
                //if (localFile.Id > 0)
                //{
                //    using (var db = new LiteDatabase(_connectionString))
                //    {
                //        if (!db.FileStorage.Exists(localFile.Id.ToString()))
                //        {
                //            var stream = new MemoryStream(localFile.Content);
                //            await Task.Factory.StartNew(() => db.FileStorage.Upload(localFile.Id.ToString(), localFile.Title, stream));
                //        }
                //        SaveToDisk(localFile);
                //    }
                //}
            }
            catch
            {
                return false;
            }
        }

        public async Task<Track> DownloadAsync(Track localFile)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var stream = new MemoryStream();
                await Task.Factory.StartNew(() => db.FileStorage.Download(localFile.Id.ToString(), stream));
                localFile.Content = stream.ToArray();
                return localFile;
            }
        }

        public async Task<IEnumerable<Track>> ListAsync()
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var stream = new MemoryStream();
                var fileInfo = await Task.Factory.StartNew(() => db.FileStorage.FindAll());
                var list = fileInfo.Select(x => new Track
                {
                    Title = x.Filename,
                    Id = int.Parse(x.Id)
                });

                return list.ToList();
            }
        }


        public async Task<bool> DeleteAsync(Track localFile)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var stream = new MemoryStream();
                var success = await Task.Factory.StartNew(() => db.FileStorage.Delete(localFile.Id.ToString()));
                DeleteFromDisk(localFile);
                return success;
            }
        }

        private bool SaveToDisk(Track queueFile)
        {
            string fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "temp.txt");

            if (Directory.Exists(queueFile.Path))
            {
                File.WriteAllBytes(queueFile.FullPath, queueFile.Content);
                return true;
            }
            return false;
        }

        private void DeleteFromDisk(Track queueFile)
        {
            if (!string.IsNullOrWhiteSpace(queueFile.FullPath) && File.Exists(queueFile.FullPath))
                File.Delete(queueFile.FullPath);
        }
    }
}
