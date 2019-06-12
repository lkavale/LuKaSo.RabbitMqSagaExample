using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Models;

namespace LuKaSo.RabbitMqSagaExample.StrategyA
{
    public class Strategy : IStrategy
    {
        public string Name
        {
            get { return "StrategyA"; }
        }

        public bool ValidateInvetsment(Investment investment)
        {
            if (investment.Amount > 100 && investment.Amount < 500 && investment.InterestRate > 6.0)
                return true;

            return false;
        }
    }
}
