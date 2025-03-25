using ECommerceSystem.Application.Services;
using System.Threading.Tasks;

namespace ECommerceSystem.Application.Services
{
    public class EmailService
    {
        private readonly IGmailService _gmailService;

        public EmailService(IGmailService gmailService)
        {
            _gmailService = gmailService;
        }

        public async Task ProcessEmailLabels()
        {
            var labels = await _gmailService.GetUserLabelsAsync();
            // Process the labels as needed
        }
    }
}