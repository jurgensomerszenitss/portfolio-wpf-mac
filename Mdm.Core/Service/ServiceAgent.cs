using Mapster;
using Mdm.Core.Models;
using Mdm.Core.Service.Auth;
using Mdm.Core.Service.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net; 
using System.Threading.Tasks;

namespace Mdm.Core.Service
{
    internal class ServiceAgent : IServiceAgent
    {
        public ServiceAgent(Config.AppSettings settings, IAuthenticator authenticator)
        {
            _settings = settings;
            _authenticator = authenticator;
        }

        private readonly Config.AppSettings _settings;
        private readonly IAuthenticator _authenticator;


        public async Task<Track> Download(Track queueFile)
        {
            try
            {
                using (var client = _authenticator.GetClient())
                { 
                    var url = new Uri(queueFile.Source); 
                    var content = await Task.Factory.StartNew(() => client.DownloadData(url));
                    queueFile.Content = content; 
                    return queueFile;
                }
            }
            catch
            {
                return null;
            } 
        } 

        public async Task<IEnumerable<QueueSheetItem>> SearchChildrenAsync(string queueSheetId = null, SortDirection sortDirection = SortDirection.Asc, string criteria = null, string[] path = null)
        {

            try
            {
                using (var client = _authenticator.GetClient())
                {
                    //path = new[] { "A", "B", "C" };
                    var url = new Uri(_settings.ApiRoot + "/queuesheets");
                    var json = await Task.Factory.StartNew(() => client.DownloadString(url));
                    
                    var jArray= JArray.Parse(json);
                    var jObject = jArray.SingleOrDefault(x => x["_id"].Value<string>() == queueSheetId);

                    jObject = GetPath(jObject, path);
                    if (jObject != null)
                    {
                        var children = GetChildrenAsync(jObject.Value<JArray>("children"), path != null && path.Any());

                        children = children.Select(x => { x.QueueSheetId = queueSheetId; return x; }).ToList();

                        var ordered = sortDirection == SortDirection.Asc ? children.OrderBy(x => x.Title).ToList() : children.OrderByDescending(x => x.Title).ToList();
                        if(!string.IsNullOrWhiteSpace(criteria)) ordered = ordered.Where(x => x.Title.ToLower().Contains(criteria.ToLower())).ToList();
                        return ordered;
                    }
                    return new List<QueueSheetItem>();
                }
            }
            catch
            {

            }

            return new List<Track>(); 
        }

        private JToken GetPath(JToken jToken, string[] path)
        {
            if (path == null || !path.Any()) return jToken;

            var index = path.First();
            var children = jToken.Value<JArray>("children");
            if (children != null)
            {
                var level = children.FirstOrDefault(x => !string.IsNullOrEmpty(x.Value<string>("title")) &&  x.Value<string>("title").Equals(index, StringComparison.InvariantCultureIgnoreCase) && !x.Children<JProperty>().Any(p => p.Name == "track"));

                if (path.Length > 1)
                    return GetPath(level, path.Skip(1).ToArray());
                return level;
            }

            return null;
        }

        public async Task<IEnumerable<QueueSheet>> SearchQueueSheetsAsync(string searchText = null)
        {
            try
            {
                using (var client = _authenticator.GetClient())
                {
                    var url = new Uri(_settings.ApiRoot + "/queuesheets");
                    var json = await Task.Factory.StartNew(() => client.DownloadString(url));
                    var list = JsonConvert.DeserializeObject<IEnumerable<QueueSheetContract>>(json);

                    var convertedList =  list.ToList().Select(x => x.Adapt<QueueSheet>());
                    if(!string.IsNullOrWhiteSpace(searchText))
                        convertedList = convertedList.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper()));
                    return convertedList;
                }
            }
            catch
            {

            }
            return new List<QueueSheet>();
        }

        public Task<QueueSheet> UpdateQueueSheetAsync(QueueSheet queueSheet)
        {
            throw new NotImplementedException();
        }

        public async Task<QueueSheet> GetQueueSheetAsync(string id)
        {
            var allQueueSheets = await SearchQueueSheetsAsync();
            return allQueueSheets.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> Unlock(QueueSheet queueSheet, int code)
        {

            try
            {
                using (var client = _authenticator.GetClient())
                {
                    var url = new Uri(_settings.ApiRoot + $"/validate/queuesheets/{queueSheet.Id}");
                    var content = new { passcode = code.ToString() };
                    var json = JsonConvert.SerializeObject(content);
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var result = await Task.FromResult(client.UploadString(url, json));
                    var jResult = JObject.Parse(result);
                    return true;
                }
            }
            catch
            {
                return false;
            } 
        }

        public async Task<bool> Authenticate(UserCredentials userCredentials)
        {
            return await _authenticator.GetToken(userCredentials.UserName, userCredentials.UserPassword);
        }

      

        public async Task<User> GetUserAsync()
        {

            return await Task.FromResult(_authenticator.User);
        }

      

        private IEnumerable<QueueSheetItem> GetChildrenAsync(JArray jArray, bool insertUp)
        {
            if (jArray == null) return new List<QueueSheetItem>();

            var children = new List<QueueSheetItem>();
            if (insertUp) children.Add(new Folder { Title = "..." });

            foreach (var jToken in jArray)
            {
                if (TryParseTrack(jToken, out Track track))
                    children.Add(track);
                else if(TryParseFolder(jToken, out Folder folder))
                    children.Add(folder);
            }

            return children;
        }

        private bool TryParseTrack(JToken jToken, out Track track)
        {
            track = null;
            if (jToken.Children<JProperty>().Any(p => p.Name == "track"))
            {
                if (jToken["track"].HasValues)
                {
                    try
                    {
                        var trackContract = jToken["track"].ToObject<TrackContract>();
                        track = trackContract.Adapt<Track>();
                        return true;
                    }
                    catch
                    {

                    }
                }
            } 
            return false;
        }

        private bool TryParseFolder(JToken jToken, out Folder folder)
        {
            folder = null;
            try
            {
                var folderContract = jToken.ToObject<FolderContract>();
                folder = folderContract.Adapt<Folder>();
                return true;
            }
            catch
            {

            }
            return false;
        }

        private IEnumerable<Track> GetTracks(JToken jToken)
        {
            var tracks = new List<Track>();
            if (jToken.Children<JProperty>().Any(p => p.Name == "track"))
            {
                if (jToken["track"].HasValues)
                {
                    try
                    {
                        var queueFileContract = jToken["track"].ToObject<TrackContract>();
                        var queueFile = queueFileContract.Adapt<Track>();
                        tracks.Add(queueFile);
                    }
                    catch
                    {

                    }
                }
            }

            if (jToken.Children<JProperty>().Any(p => p.Name == "children") && jToken["children"].HasValues)
            {
                var jArray = (JArray)jToken["children"];
                foreach (var jChildToken in jArray)
                {
                    tracks.AddRange(GetTracks(jChildToken));
                }
            }
            return tracks;
        } 
    }
}
