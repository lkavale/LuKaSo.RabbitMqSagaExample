using LuKaSo.RabbitMqSagaExample.Common.Models;
using LuKaSo.RabbitMqSagaExample.Contracts.Broker;
using MassTransit;
using Quartz;
using System;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.Broker.Jobs
{
    [DisallowConcurrentExecution()]
    public class NewInvestmentJob : IJob
    {
        /// <summary>
        /// Bus
        /// </summary>
        protected IBus _bus;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bus"></param>
        public NewInvestmentJob(IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Exectute job
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            var random = new Random();

            var investment = new InvestmentOffer()
            {
                Id = Guid.NewGuid(),
                Amount = (decimal)random.NextDouble() * 1000,
                InterestRate = random.Next(50, 100) / 10.0
            };

            return _bus.Publish<IInvestmentOfferNew>(new { Investment = investment });
        }
    }
}
