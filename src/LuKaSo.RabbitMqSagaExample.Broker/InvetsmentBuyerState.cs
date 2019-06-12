using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Models;
using System;

namespace LuKaSo.RabbitMqSagaExample.Broker
{
    public class InvetsmentBuyerState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public Investment Investment { get; set; }

        public State State { get; set; }

        public CompositeEventStatus BuyApprovementState { get; set; }
    }
}
