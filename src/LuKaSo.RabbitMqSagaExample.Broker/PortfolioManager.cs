using LuKaSo.RabbitMqSagaExample.Common.Extensions;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LuKaSo.RabbitMqSagaExample.Broker
{
    public class PortfolioManager : IPortfolioManager
    {
        /// <summary>
        /// List of owned investments
        /// </summary>
        private readonly List<Investment> _portfolio;

        /// <summary>
        /// Portfolio manager
        /// </summary>
        public PortfolioManager()
        {
            _portfolio = new List<Investment>();
        }

        /// <summary>
        /// Add investments offer into portfolio
        /// </summary>
        /// <param name="investment"></param>
        public void Add(InvestmentOffer investment)
        {
            if (_portfolio.Any(i => i.Id == investment.Id))
                throw new InvalidOperationException("Investment has been already added");

            _portfolio.Add(new Investment(investment));
        }

        /// <summary>
        /// Get all owned invetsments
        /// </summary>
        /// <returns></returns>
        public Collection<Investment> GetInvestments()
        {
            return new Collection<Investment>(_portfolio.DeepClone());
        }

        /// <summary>
        /// Remove investments from the portfolio
        /// </summary>
        /// <param name="id"></param>
        public void Remove(Guid id)
        {
            var investment = _portfolio.SingleOrDefault(i => i.Id == id);

            if (investment == null)
                throw new InvalidOperationException($"Portfolio not contains investment with id {id}");

            _portfolio.Remove(investment);
        }
    }
}
