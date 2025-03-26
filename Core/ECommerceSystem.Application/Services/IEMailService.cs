    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ECommerceSystem.Application.Services
    {
        public interface IEMailService
        {
            Task<bool> SendEmailAsync(string to, string subject, string body);
        }
    }
