using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.OrderManagement
{
    public interface IInvestmentOfferOrderManagementRevoked
    {
        InvestmentOffer Investment { get; set; }
    }
}
