using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Common.Models;
using System;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement.StateMachine
{
    public class OrderManagementInvestmentState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public InvestmentOffer Investment { get; set; }

        public State State { get; set; }

        public CompositeEventStatus ApprovementState { get; set; }
    }
}
