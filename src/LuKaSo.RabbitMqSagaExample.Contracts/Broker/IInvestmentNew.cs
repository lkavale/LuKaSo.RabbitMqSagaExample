using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Broker
{
    public interface IInvestmentNew
    {
        Investment Investment { get; set; }
    }
}
