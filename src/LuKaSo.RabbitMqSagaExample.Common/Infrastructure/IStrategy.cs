using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IStrategy
    {
        /// <summary>
        /// Strategy name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Validate investments
        /// </summary>
        /// <param name="investment"></param>
        /// <returns></returns>
        bool ValidateInvetsment(InvestmentOffer investment);
    }
}
