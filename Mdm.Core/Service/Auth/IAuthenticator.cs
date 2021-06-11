using Mdm.Core.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mdm.Core.Service.Auth
{
    public interface IAuthenticator
    {
        User User { get; }
        WebClient GetClient();
        void AddAuthHeader(WebClient webClient);
        Task<bool> GetToken(string userName, string password);
    }
}
