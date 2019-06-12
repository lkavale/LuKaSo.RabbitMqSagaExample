using LuKaSo.RabbitMqSagaExample.Contracts.Broker;
using LuKaSo.RabbitMqSagaExample.OrderManagement.Logging;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement
{
    public class PortfolioConsumer : IConsumer<IPortfolio>
    {
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Portfolio consumer
        /// </summary>
        public PortfolioConsumer()
        {
            _log = LogProvider.For<PortfolioConsumer>();
        }

        public async Task Consume(ConsumeContext<IPortfolio> context)
        {
            await Task.Run(() => _log.Debug($"Received new portfolio composition, portfolio contains {context.Message.Invetsments.Count()}"));
        }
    }
}
