using Google.Apis.Gmail.v1.Data; // Add this line
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.Application.Services
{
    public interface IGmailService
    {
        // Use Google's Label type here
        Task<IList<Label>> GetUserLabelsAsync();
    }
}