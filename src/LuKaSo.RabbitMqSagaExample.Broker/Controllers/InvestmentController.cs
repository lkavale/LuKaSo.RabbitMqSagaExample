using LuKaSo.RabbitMqSagaExample.Broker.Logging;
using LuKaSo.RabbitMqSagaExample.Common.Models;
using LuKaSo.RabbitMqSagaExample.Contracts.Broker;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.Broker.Controllers
{
    [ApiController]
    [Route("api/investment")]
    public class InvestmentController : ControllerBase
    {
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Bus
        /// </summary>
        private readonly IBus _bus;

        /// <summary>
        /// Investment controller
        /// </summary>
        /// <param name="bus"></param>
        public InvestmentController(IBus bus)
        {
            _log = LogProvider.For<InvestmentController>();
            _bus = bus;
        }

        /// <summary>
        /// Send a new investment to bus
        /// </summary>
        /// <param name="investment"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SendInvestment(InvestmentOffer investment)
        {
            _log.Debug($"Sending a new investment {investment}");

            try
            {
                investment.Id = Guid.NewGuid();

                await _bus.Publish<IInvestmentOfferNew>(new { Investment = investment });

                return Ok();
            }
            catch (Exception exp)
            {
                _log.Error(exp, $"Error while publishing new investment.");
            }

            return StatusCode(500);
        }
    }
}
