using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Contracts.Broker;
using LuKaSo.RabbitMqSagaExample.Contracts.OrderManagement;
using LuKaSo.RabbitMqSagaExample.OrderManagement.Logging;
using System;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement
{
    public class OrderManagementStateMachine : MassTransitStateMachine<OrderManagementInvestmentState>
    {
        /// <summary>
        /// Investment approved by the order management
        /// </summary>
        public State OrderManagementApproved { get; private set; }

        /// <summary>
        /// Investment revoked by the order management
        /// </summary>
        public State OrderManagementRevoked { get; private set; }

        /// <summary>
        /// New investment received event
        /// </summary>
        public Event<IInvestmentNew> InvestmentNew { get; private set; }

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Order management state machine
        /// </summary>
        /// <param name="orderManagement"></param>
        public OrderManagementStateMachine(IOrderManagement orderManagement)
        {
            _log = LogProvider.For<OrderManagementStateMachine>();

            State(() => OrderManagementApproved);
            State(() => OrderManagementRevoked);

            Event(() => InvestmentNew, x => x.CorrelateById(os => os.Investment.Id, ctx => ctx.Message.Investment.Id).SelectId(context => Guid.NewGuid()));

            Initially(
                When(InvestmentNew)
                    // Set received Investment to machine state
                    .Then(ctx => ctx.Instance.Investment = ctx.Data.Investment)
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} added to order management")))
                    .IfElse(ctx => orderManagement.ValidateInvetsment(ctx.Instance.Investment),
                        ctx => ctx.TransitionTo(OrderManagementApproved),
                        ctx => ctx.TransitionTo(OrderManagementRevoked)));

            DuringAny(
                // Investment approved by order management
                When(OrderManagementApproved.Enter)
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} approved by order management")))
                    // Send approved message
                    .Then(ctx => ctx.Publish<OrderManagementInvestmentState, IInvestmentOrderManagementApproved>(new
                    {
                        Investment = ctx.Instance.Investment
                    }))
                    .Finalize(),

                // Investment revoked by order management
                When(OrderManagementRevoked.Enter)
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} revoked by order management")))
                    // Send revoked message
                    .Then(ctx => ctx.Publish<OrderManagementInvestmentState, IInvestmentOrderManagementRevoked>(new
                    {
                        Investment = ctx.Instance.Investment
                    }))
                    .Finalize());

            SetCompletedWhenFinalized();
        }
    }
}
