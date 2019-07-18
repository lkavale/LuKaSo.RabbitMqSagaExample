using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Strategy.Controllers
{
    public abstract class StrategyBase : IStrategy
    {
        private readonly IConfigurationManager<StrategyConfig> _configurationManager;

        protected StrategyBase(IConfigurationManager<StrategyConfig> configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public abstract string Name { get; }

        public bool ValidateInvetsment(InvestmentOffer investment)
        {
            var config = _configurationManager.Read();

            return (config.AmountFrom == null || investment.Amount >= config.AmountFrom)
                && (config.AmountTo == null || investment.Amount < config.AmountTo)
                && (config.InterestRateFrom == null || investment.InterestRate > config.InterestRateFrom)
                && (config.InterestRateTo == null || investment.InterestRate < config.InterestRateTo);
        }
    }
}
