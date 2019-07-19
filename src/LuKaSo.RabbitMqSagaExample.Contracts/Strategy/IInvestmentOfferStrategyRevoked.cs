using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Strategy
{
    public interface IInvestmentOfferStrategyRevoked
    {
        /// <summary>
        /// Revoked investments
        /// </summary>
        InvestmentOffer Investment { get; set; }

        /// <summary>
        /// Strategy name
        /// </summary>
        string StrategyName { get; set; }
    }
}
