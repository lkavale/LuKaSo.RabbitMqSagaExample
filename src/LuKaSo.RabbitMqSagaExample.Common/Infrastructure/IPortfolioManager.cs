using LuKaSo.RabbitMqSagaExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IPortfolioManager
    {
        void Add(Investment investment);

        void Remove(Guid id);

        List<Investment> GetInvestments();
    }
}
