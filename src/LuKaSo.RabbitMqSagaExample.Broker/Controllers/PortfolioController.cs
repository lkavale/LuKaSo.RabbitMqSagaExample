using LuKaSo.RabbitMqSagaExample.Broker.Logging;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LuKaSo.RabbitMqSagaExample.Broker.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Portfolio manager
        /// </summary>
        private readonly IPortfolioManager _portfolioManager;

        /// <summary>
        /// Portfolio controller
        /// </summary>
        /// <param name="portfolioManager"></param>
        public PortfolioController(IPortfolioManager portfolioManager)
        {
            _log = LogProvider.For<PortfolioController>();
            _portfolioManager = portfolioManager;
        }

        /// <summary>
        /// Get portfolio
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Collection<Investment>> GetPortfolio()
        {
            _log.Debug($"Get portfolio requested");

            return _portfolioManager.GetInvestments();
        }

        /// <summary>
        /// Get portfolio statistics
        /// </summary>
        /// <returns></returns>
        [HttpGet("/statistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<InvestmentOffer>> GetPortfolioStatistics()
        {
            _log.Debug($"Get portfolio statistics requested");

            throw new NotImplementedException();
        }
    }
}
