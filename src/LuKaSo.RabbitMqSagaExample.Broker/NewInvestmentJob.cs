using LuKaSo.RabbitMqSagaExample.Contracts.Broker;
using LuKaSo.RabbitMqSagaExample.Models;
using MassTransit;
using Quartz;
using System;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.Broker
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

            var investment = new Investment()
            {
                Id = Guid.NewGuid(),
                Amount = (decimal)random.NextDouble() * 1000,
                InterestRate = random.Next(50, 100) / 10
            };

            return _bus.Publish<IInvestmentNew>(new { Investment = investment });
        }
    }
}
