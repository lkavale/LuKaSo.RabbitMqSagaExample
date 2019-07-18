using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Common.Models;
using System;

namespace LuKaSo.RabbitMqSagaExample.Strategy
{
    public class StrategyInvestmentState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public InvestmentOffer Investment { get; set; }

        public State State { get; set; }
    }
}
