using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IOrderManagement
    {
        bool ValidateInvetsment(Investment investment);
    }
}
