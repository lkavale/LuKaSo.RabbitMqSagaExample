using LuKaSo.RabbitMqSagaExample.Common.Extensions;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LuKaSo.RabbitMqSagaExample.Broker
{
    public class PortfolioManager : IPortfolioManager
    {
        private readonly List<Investment> _portfolio;

        public PortfolioManager()
        {
            _portfolio = new List<Investment>();
        }

        public void Add(Investment investment)
        {
            if (_portfolio.Any(i => i.Id == investment.Id))
                throw new InvalidOperationException("Investment has been already added");

            _portfolio.Add(investment);
        }

        public List<Investment> GetInvestments()
        {
            return _portfolio.DeepClone();
        }

        public void Remove(Guid id)
        {
            var investment = _portfolio.SingleOrDefault(i => i.Id == id);

            if (investment == null)
                throw new InvalidOperationException($"Portfolio not contains investment with id {id}");

            _portfolio.Remove(investment);
        }
    }
}
