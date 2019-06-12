using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.StrategyB
{
    public class Strategy : IStrategy
    {
        public string Name
        {
            get { return "StrategyB"; }
        }

        public bool ValidateInvetsment(Investment investment)
        {
            if (investment.Amount > 300 && investment.Amount < 800 && investment.InterestRate > 7.0)
                return true;

            return false;
        }
    }
}
