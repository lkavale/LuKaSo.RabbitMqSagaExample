using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Common.Models;
using System;

namespace LuKaSo.RabbitMqSagaExample.Broker.StateMachine
{
    public class InvetsmentBuyerState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public InvestmentOffer Investment { get; set; }

        public State State { get; set; }

        public CompositeEventStatus BuyApprovementState { get; set; }
    }
}
