using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.OrderManagement
{
    public interface IInvestmentOrderManagementRevoked
    {
        Investment Investment { get; set; }
    }
}
