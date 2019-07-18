using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Contracts.Broker;
using LuKaSo.RabbitMqSagaExample.OrderManagement.Logging;
using MassTransit;
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
        /// Order management
        /// </summary>
        private readonly IOrderManagement _orderManagement;

        /// <summary>
        /// Portfolio consumer
        /// </summary>
        public PortfolioConsumer(IOrderManagement orderManagement)
        {
            _log = LogProvider.For<PortfolioConsumer>();
            _orderManagement = orderManagement;
        }

        /// <summary>
        /// Consume message context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<IPortfolio> context)
        {
            await Task.Run(() => Consume(context.Message));
        }

        /// <summary>
        /// Consume message
        /// </summary>
        /// <param name="portfolio"></param>
        private void Consume(IPortfolio portfolio)
        {
            _log.Debug($"Received new portfolio composition, portfolio contains {portfolio.Investments.Count}");

            _orderManagement.Update(portfolio.Investments);
        }
    }
}
