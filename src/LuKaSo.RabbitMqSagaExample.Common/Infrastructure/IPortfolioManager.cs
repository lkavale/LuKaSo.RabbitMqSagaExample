using LuKaSo.RabbitMqSagaExample.Common.Models;
using System;
using System.Collections.ObjectModel;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IPortfolioManager
    {
        /// <summary>
        /// Add investments into portfolio
        /// </summary>
        /// <param name="investment"></param>
        void Add(InvestmentOffer investment);

        /// <summary>
        /// Remove investments into portfolio
        /// </summary>
        /// <param name="id"></param>
        void Remove(Guid id);

        /// <summary>
        /// Get all investments
        /// </summary>
        /// <returns></returns>
        Collection<Investment> GetInvestments();
    }
}
