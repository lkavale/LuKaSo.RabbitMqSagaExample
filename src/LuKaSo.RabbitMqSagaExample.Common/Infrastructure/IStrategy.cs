using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IStrategy
    {
        string Name { get; }

        bool ValidateInvetsment(InvestmentOffer investment);
    }
}
