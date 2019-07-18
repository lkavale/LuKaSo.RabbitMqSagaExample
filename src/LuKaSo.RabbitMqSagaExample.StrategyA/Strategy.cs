using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Strategy;
using LuKaSo.RabbitMqSagaExample.Strategy.Controllers;

namespace LuKaSo.RabbitMqSagaExample.StrategyA
{
    public class Strategy : StrategyBase
    {
        public Strategy(IConfigurationManager<StrategyConfig> configurationManager) : base(configurationManager)
        {
        }

        public override string Name
        {
            get { return "StrategyA"; }
        }
    }
}
