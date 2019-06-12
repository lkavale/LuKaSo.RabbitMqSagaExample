using LuKaSo.RabbitMqSagaExample.Models;
using System.Collections.Generic;

namespace LuKaSo.RabbitMqSagaExample.Contracts.Broker
{
    public interface IPortfolio
    {
        List<Investment> Invetsments { get; set; }
    }
}
