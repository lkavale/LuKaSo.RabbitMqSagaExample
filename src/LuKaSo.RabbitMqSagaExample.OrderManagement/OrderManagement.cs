using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement
{
    public class OrderManagement : IOrderManagement
    {
        public bool ValidateInvetsment(Investment investment)
        {
            return true;
        }
    }
}
