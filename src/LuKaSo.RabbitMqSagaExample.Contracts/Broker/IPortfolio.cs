using LuKaSo.RabbitMqSagaExample.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Broker
{
    public interface IPortfolio
    {
        Collection<Investment> Investments { get; set; }
    }
}
