using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Strategy
{
    public interface IInvestmentStrategyApproved
    {
        Investment Investment { get; set; }

        string StrategyName { get; set; }
    }
}
