using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Application.Services
{
    public interface IMessagingService
    {
        void Publish<T>(T message, string queueName);

    }
}
