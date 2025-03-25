// Application/UseCases/GetUserLabelsHandler.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities;
using ECommerceSystem.Application.Services;
using Google.Apis.Gmail.v1.Data;

namespace Infrastructure.UseCases
{
    public class GetUserLabelsHandler
    {
        private readonly IGmailService _gmailService;

        public GetUserLabelsHandler(IGmailService gmailService)
        {
            _gmailService = gmailService;
        }

        public async Task<IList<Label>> Handle()
        {
            return await _gmailService.GetUserLabelsAsync();
        }
    }
}