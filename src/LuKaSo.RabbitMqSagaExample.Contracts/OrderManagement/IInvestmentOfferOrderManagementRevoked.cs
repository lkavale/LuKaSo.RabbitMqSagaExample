using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.OrderManagement
{
    public interface IInvestmentOfferOrderManagementRevoked
    {
        /// <summary>
        /// Revoked investment
        /// </summary>
        InvestmentOffer Investment { get; set; }
    }
}
