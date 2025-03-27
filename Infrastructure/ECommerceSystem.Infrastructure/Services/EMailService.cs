using System.Net;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Google.Apis.Auth.OAuth2;
using ECommerceSystem.Application.Services;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

namespace ECommerceSystem.Infrastructure.Services
{
    public class EMailService : IEMailService
    {
        private readonly IConfiguration _configuration;

        public EMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string receptor, string subject, string body)
        {
            try
            {
                // Get OAuth credentials
                var clientId = _configuration["EmailSettings:OAuthClientId"];
                var clientSecret = _configuration["EmailSettings:OAuthClientSecret"];
                var refreshToken = _configuration["EmailSettings:OAuthRefreshToken"];
                var senderEmail = _configuration["EmailSettings:Email"]; // Ensure this exists in appsettings.json

                // Create Google credentials
                var clientSecrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                };

                var credential = new UserCredential(
                    new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                    {
                        ClientSecrets = clientSecrets
                    }),
                    "user",
                    new TokenResponse { RefreshToken = refreshToken });

                // Get access token
                var accessToken = await credential.GetAccessTokenForRequestAsync();

                // Create email message
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("ECommerceSite", senderEmail));
                message.To.Add(new MailboxAddress("", receptor));
                message.Subject = subject;

                // Inline CSS styling added to HTML email body
                var htmlBody = $@"
<html>
<head>
  <style>
    body {{
      font-family: Arial, sans-serif;
      background-color: #f4f4f4;
      padding: 20px;
    }}
    .container {{
      background-color: #ffffff;
      padding: 20px;
      border-radius: 5px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }}
    h1 {{
      color: #333333;
    }}
    p {{
      color: #666666;
      line-height: 1.5;
    }}
  </style>
</head>
<body>
  <div class='container'>
    {body}
  </div>
</body>
</html>";

                // Note: Changed TextPart from "plain" to "html" only for styling purposes.
                message.Body = new TextPart("html") { Text = htmlBody };

                // Send via SMTP with OAuth
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    _configuration["EmailSettings:Host"],
                    int.Parse(_configuration["EmailSettings:Port"]),
                    SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(new SaslMechanismOAuth2(senderEmail, accessToken));
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
