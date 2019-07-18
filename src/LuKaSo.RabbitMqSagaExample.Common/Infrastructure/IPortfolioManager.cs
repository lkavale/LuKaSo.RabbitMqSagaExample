using LuKaSo.RabbitMqSagaExample.Common.Models;
using System;
using System.Collections.ObjectModel;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IPortfolioManager
    {
        void Add(InvestmentOffer investment);

        void Remove(Guid id);

        Collection<Investment> GetInvestments();
    }
}
