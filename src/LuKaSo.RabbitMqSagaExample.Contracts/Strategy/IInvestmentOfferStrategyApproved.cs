using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Strategy
{
    public interface IInvestmentOfferStrategyApproved
    {
        /// <summary>
        /// Approved investment
        /// </summary>
        InvestmentOffer Investment { get; set; }

        /// <summary>
        /// Strategy name
        /// </summary>
        string StrategyName { get; set; }
    }
}
