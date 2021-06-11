using LiteDB;
using Mdm.Core.Extensions;
using Mdm.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mdm.Core.Repository
{
    internal class Repository : IRepository
    {
        public Repository(Config.AppSettings settings)
        {
            _settings = settings;
            _connectionString =$"Filename={settings.LocalFolder}{settings.Database}";
        }

        private readonly string _connectionString;
        private readonly Config.AppSettings _settings;
        private const string LOCALFILES = "localfiles";
        private const string SETTINGS = "settings";
        private const string CREDENTIALS = "credentials";

        public async Task<Track> CreateAsync(Track localFile)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<Track>(LOCALFILES);
                if (collection.Exists(x => x.Id == localFile.Id))
                {
                    await Task.Factory.StartNew(() => collection.Update(localFile));
                }
                else
                {
                    await Task.Factory.StartNew(() => collection.Insert(localFile));
                }                
                return localFile;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<Track>(LOCALFILES);
                return await Task.Factory.StartNew(() => collection.Delete(id));
            }
        }

        public async Task<Track> GetAsync(int id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<Track>(LOCALFILES);
                return await Task.Factory.StartNew(() => collection.FindOne(x => x.Id == id));
            }
        }

        public async Task<IEnumerable<Track>> SearchAsync(string queueSheetId = null)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<Track>(LOCALFILES);
                if(!string.IsNullOrWhiteSpace(queueSheetId))
                {
                    return await collection.Find(Query.EQ(nameof(Track.QueueSheetId), queueSheetId)).ToListAsync();
                }
                else
                {
                    return await collection.FindAll().ToListAsync();
                } 
               
            }
        }

        public async Task<IEnumerable<Track>> SearchExpiredAsync()
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<Track>(LOCALFILES);
                return await collection.Find(Query.LTE(nameof(Track.ExpiresAt), DateTime.Now)).ToListAsync();
            }
        }

        public async Task<UserSettings> GetUserSettingsAsync(string userName)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<UserSettings>(SETTINGS);

                var settings = collection.Find(Query.EQ(nameof(UserSettings.UserName), userName.ToUpper())).FirstOrDefault();
                if(settings == null)
                {
                    settings = new UserSettings();
                    settings.Id = Guid.NewGuid().ToString();
                    collection.Insert(settings);
                    return settings;
                }

                //if (settings.Count > 1)
                //{
                //    var settings = settings.First();
                //    collection.DeleteAll();
                //    collection.Insert(settings);
                //}
                return await Task.FromResult(settings);
            }
        }

        public async Task<UserSettings> UpdateUserSettingsAsync(UserSettings settings)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<UserSettings>(SETTINGS);
                settings.UserName = settings.UserName.ToUpper();
                await Task.Factory.StartNew(() => collection.Update(settings));
                return settings;
            }
        }

        public async Task<UserCredentials> SetUserCredentialsAsync(UserCredentials userCredentials)
        {
            using (var db = new LiteDatabase(_connectionString))
            { 
                var collection = db.GetCollection<UserCredentials>(CREDENTIALS);
                collection.DeleteAll();
                if(userCredentials != null)  await Task.Factory.StartNew(() => collection.Insert(userCredentials));
                return userCredentials;
            }
        }

        public async Task<UserCredentials> GetUserCredentialsAsync()
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<UserCredentials>(CREDENTIALS);
                var credentials = await collection.FindAll().ToListAsync();
                return credentials.FirstOrDefault();
            }
        }

        public async Task<string> GetForgotPasswordUrlAsync()
        {
            return await Task.FromResult(_settings.ForgotPasswordUrl);
        }
    }
}
