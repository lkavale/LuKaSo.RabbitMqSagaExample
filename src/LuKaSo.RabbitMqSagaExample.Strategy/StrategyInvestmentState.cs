using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Models;
using System;

namespace LuKaSo.RabbitMqSagaExample.Strategy
{
    public class StrategyInvestmentState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public Investment Investment { get; set; }

        public State State { get; set; }
    }
}
