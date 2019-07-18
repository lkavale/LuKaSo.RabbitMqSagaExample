using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Strategy
{
    public interface IInvestmentOfferStrategyApproved
    {
        InvestmentOffer Investment { get; set; }

        string StrategyName { get; set; }
    }
}
