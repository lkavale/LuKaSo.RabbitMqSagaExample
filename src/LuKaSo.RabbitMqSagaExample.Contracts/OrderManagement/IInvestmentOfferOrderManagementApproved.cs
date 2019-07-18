using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.OrderManagement
{
    public interface IInvestmentOfferOrderManagementApproved
    {
        InvestmentOffer Investment { get; set; }

        decimal Amount { get; set; }
    }
}
