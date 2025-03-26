using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Services;
using Microsoft.Extensions.Configuration;

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


                var email = _configuration.GetValue<string>("EmailSettings:Email");
                var password = _configuration.GetValue<string>("EmailSettings:Password");
                var host = _configuration.GetValue<string>("EmailSettings:Host");
                var port = _configuration.GetValue<int>("EmailSettings:Port");

                var smpClient = new SmtpClient(host, port);
                smpClient.EnableSsl = true;
                smpClient.UseDefaultCredentials = false;

                smpClient.Credentials = new System.Net.NetworkCredential(email, password);

                var message = new MailMessage(email, receptor, subject, body);
                await smpClient.SendMailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
            


        }
    }
}
