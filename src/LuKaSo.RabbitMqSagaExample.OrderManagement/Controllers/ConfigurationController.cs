using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.OrderManagement.Logging;
using LuKaSo.RabbitMqSagaExample.OrderManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement.Controllers
{
    [ApiController]
    [Route("api/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationManager<List<OrderManagementConfig>> _configManager;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        public ConfigurationController(IConfigurationManager<List<OrderManagementConfig>> configManager)
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
        public ActionResult<List<OrderManagementConfig>> GetConfiguration()
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
        public ActionResult UpdateConfiguration(List<OrderManagementConfig> config)
        {
            try
            {
                _configManager.Write(config);
                return Ok();
            }
            catch (Exception exp)
            {
                _log.Error(exp, $"Error while writing order management configuration.");
            }

            return StatusCode(500);
        }
    }
}
