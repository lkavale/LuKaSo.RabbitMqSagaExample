using LuKaSo.RabbitMqSagaExample.Common.Models;
using System.Collections.Generic;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IOrderManagement
    {
        /// <summary>
        /// Validate investment
        /// </summary>
        /// <param name="investment"></param>
        /// <returns></returns>
        bool ValidateInvetsment(InvestmentOffer investment);

        /// <summary>
        /// Update owned investments
        /// </summary>
        /// <param name="investments"></param>
        void Update(ICollection<Investment> investments);
    }
}
