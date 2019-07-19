using LuKaSo.RabbitMqSagaExample.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Broker
{
    public interface IPortfolio
    {
        /// <summary>
        /// Owned portfolio of investments
        /// </summary>
        Collection<Investment> Investments { get; set; }
    }
}
