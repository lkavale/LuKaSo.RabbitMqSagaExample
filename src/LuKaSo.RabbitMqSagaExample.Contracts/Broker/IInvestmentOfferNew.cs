using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Broker
{
    public interface IInvestmentOfferNew
    {
        InvestmentOffer Investment { get; set; }
    }
}
