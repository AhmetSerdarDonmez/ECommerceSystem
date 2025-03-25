using System.Threading;
using System.Threading.Tasks;
using ECommerceSystem.Application.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;

namespace ECommerceSystem.Infrastructure.Services
{
    public class GmailApiService : IGmailService // Renamed
    {
        private readonly Google.Apis.Gmail.v1.GmailService _gmailService; // Fully qualified

        public GmailApiService(IConfiguration configuration)
        {
            var clientId = configuration["GmailApi:ClientId"];
            var clientSecret = configuration["GmailApi:ClientSecret"];
            var appName = configuration["GmailApi:ApplicationName"];

            // Initialize asynchronously
            var credential = GetCredentialAsync(clientId, clientSecret).GetAwaiter().GetResult();

            _gmailService = new Google.Apis.Gmail.v1.GmailService(
                new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = appName
                });
        }

        private async Task<UserCredential> GetCredentialAsync(string clientId, string clientSecret)
        {
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret },
                new[] { Google.Apis.Gmail.v1.GmailService.Scope.GmailReadonly },
                "user",
                CancellationToken.None,
                new FileDataStore("token.json", true));
        }

        public async Task<IList<Label>> GetUserLabelsAsync()
        {
            var request = _gmailService.Users.Labels.List("me");
            var response = await request.ExecuteAsync();
            return response.Labels ?? new List<Label>(); // Handle null
        }
    }
}