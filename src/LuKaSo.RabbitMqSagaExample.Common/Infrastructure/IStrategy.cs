using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IStrategy
    {
        string Name { get; }

        bool ValidateInvetsment(Investment investment);
    }
}
