using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Strategy.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LuKaSo.RabbitMqSagaExample.Strategy.Controllers
{
    [ApiController]
    [Route("api/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationManager<StrategyConfig> _configManager;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        public ConfigurationController(IConfigurationManager<StrategyConfig> configManager)
        {
            _log = LogProvider.For<ConfigurationController>();
            _configManager = configManager;
        }

        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<StrategyConfig> GetConfiguration()
        {
            var config = _configManager.Read();

            if (config == null)
            {
                return NoContent();
            }

            return _configManager.Read();
        }

        /// <summary>
        /// Update configuration
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateConfiguration(StrategyConfig config)
        {
            try
            {
                _configManager.Write(config);
                return Ok();
            }
            catch (Exception exp)
            {
                _log.Error(exp, $"Error while writing strategy configuration.");
            }

            return StatusCode(500);
        }
    }
}
