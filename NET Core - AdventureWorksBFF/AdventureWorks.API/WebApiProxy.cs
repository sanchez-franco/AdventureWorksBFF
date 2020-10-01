using AdventureWorks.Common.Configuration;
using AdventureWorks.Common.Model;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AdventureWorks.API
{
    public interface IWebApiProxy
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);
        Task<PersonDetail> GetPersonDetail(string token, int personId);
        Task<PersonDetail[]> GetPersonDetails(string token);
    }

    public class WebApiProxy : IWebApiProxy
    {
        private readonly AppSettings _appSettings;

        public WebApiProxy(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest)
        {
            var client = new WebApiClient();

            return await client.Post<AuthenticateRequest, AuthenticateResponse>(_appSettings.AuthenticationUrl, "api/authentication/authenticate", authenticateRequest);
        }

        public async Task<PersonDetail> GetPersonDetail(string token, int personId)
        {
            var client = new WebApiClient(token);

            return await client.GetAsync<PersonDetail>(_appSettings.PersonUrl, "api/person", personId.ToString());
        }

        public async Task<PersonDetail[]> GetPersonDetails(string token)
        {
            var client = new WebApiClient(token);

            return await client.GetAsync<PersonDetail[]>(_appSettings.PersonUrl, "api/person");
        }
    }
}