using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.OrderManagement
{
    public interface IInvestmentOfferOrderManagementApproved
    {
        /// <summary>
        /// Approved investments
        /// </summary>
        InvestmentOffer Investment { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        decimal Amount { get; set; }
    }
}
