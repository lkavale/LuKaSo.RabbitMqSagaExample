using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Models;
using System;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement
{
    public class OrderManagementInvestmentState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public Investment Investment { get; set; }

        public State State { get; set; }

        public CompositeEventStatus ApprovementState { get; set; }
    }
}
