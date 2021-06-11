using Mapster;
using Mdm.Core.Models;
using Mdm.Core.Service.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Linq;

namespace Mdm.Core.Service.Auth
{
    internal class Authenticator : IAuthenticator
    {
        public Authenticator(Config.AppSettings settings)
        {
            _settings = settings;
        }

        private readonly Config.AppSettings _settings;
        private string _token;

        public User User { get; private set; } 


        public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

        public WebClient GetClient()
        {
            var webClient = new WebClient();
            AddAuthHeader(webClient);
            return webClient;

        }      

        public void AddAuthHeader(WebClient webClient)
        {
            if(IsAuthenticated)   
                webClient.Headers.Add("x-auth", _token);
        }

        public async Task<bool> GetToken(string userName, string password)
        {
            using (var client = new WebClient())
            {
                try
                {
                    var url = new Uri(_settings.ApiRoot + "/users/login");
                    var content = new { username = userName, password = password };
                    var json = JsonConvert.SerializeObject(content);
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var result = await Task.FromResult(client.UploadString(url, json));
                    var jResult = JObject.Parse(result);
                    _token = jResult.Value<string>("token"); 

                    User = jResult.Value<JObject>("user").ToObject<UserContract>().Adapt<User>();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task.Factory.StartNew(SendMacAddressAsync);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task SendMacAddressAsync()
        {
            try
            {
                var macAddress = GetMacAddress();
                using (var client = GetClient())
                {
                    var url = new Uri(_settings.ApiRoot + $"/devices/status");
                    var content = new { mac_address = macAddress, device_name = Environment.MachineName, status = "offline" };
                    var json = JsonConvert.SerializeObject(content);
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var result = await Task.FromResult(client.UploadString(url, WebRequestMethods.Http.Put, json));
                    var jResult = JObject.Parse(result);
                }
            }
            catch
            {

            }
        }

        private string GetMacAddress()
        {
            var macAddr =
            (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();

            return macAddr;
        }

    }
}
