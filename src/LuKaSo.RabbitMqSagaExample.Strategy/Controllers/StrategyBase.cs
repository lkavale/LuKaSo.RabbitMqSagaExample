using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Common.Models;

namespace LuKaSo.RabbitMqSagaExample.Strategy.Controllers
{
    public abstract class StrategyBase : IStrategy
    {
        /// <summary>
        /// Configuration manager
        /// </summary>
        private readonly IConfigurationManager<StrategyConfig> _configurationManager;

        /// <summary>
        /// Strategy base
        /// </summary>
        /// <param name="configurationManager"></param>
        protected StrategyBase(IConfigurationManager<StrategyConfig> configurationManager)
        {
            _configurationManager = configurationManager;
        }

        /// <summary>
        /// Strategy name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Validate investment
        /// </summary>
        /// <param name="investment"></param>
        /// <returns></returns>
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
