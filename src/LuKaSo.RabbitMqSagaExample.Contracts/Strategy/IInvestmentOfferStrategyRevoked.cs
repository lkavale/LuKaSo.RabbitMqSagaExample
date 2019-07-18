using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Strategy
{
    public interface IInvestmentOfferStrategyRevoked
    {
        InvestmentOffer Investment { get; set; }

        string StrategyName { get; set; }
    }
}
