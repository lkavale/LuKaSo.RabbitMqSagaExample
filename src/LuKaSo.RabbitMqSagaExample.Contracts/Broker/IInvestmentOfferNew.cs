using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Broker
{
    public interface IInvestmentOfferNew
    {
        /// <summary>
        /// Invetment
        /// </summary>
        InvestmentOffer Investment { get; set; }
    }
}
