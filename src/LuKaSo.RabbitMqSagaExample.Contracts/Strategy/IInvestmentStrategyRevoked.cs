using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Strategy
{
    public interface IInvestmentStrategyRevoked
    {
        Investment Investment { get; set; }

        string StrategyName { get; set; }
    }
}
