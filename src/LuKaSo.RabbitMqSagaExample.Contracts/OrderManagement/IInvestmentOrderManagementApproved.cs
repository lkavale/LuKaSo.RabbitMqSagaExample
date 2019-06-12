using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.Contracts.OrderManagement
{
    public interface IInvestmentOrderManagementApproved
    {
        Investment Investment { get; set; }

        decimal Amount { get; set; }
    }
}
