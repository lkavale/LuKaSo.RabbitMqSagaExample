using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Contracts.Broker;
using LuKaSo.RabbitMqSagaExample.Contracts.Strategy;
using LuKaSo.RabbitMqSagaExample.Strategy.Logging;
using System;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.Strategy
{
    public sealed class StrategyStateMachine : MassTransitStateMachine<StrategyInvestmentState>
    {
        /// <summary>
        /// Investment approved by the strategy
        /// </summary>
        public State StrategyApproved { get; private set; }

        /// <summary>
        /// Investment revoked by the strategy
        /// </summary>
        public State StrategyRevoked { get; private set; }

        /// <summary>
        /// New investment received event
        /// </summary>
        public Event<IInvestmentNew> InvestmentNew { get; private set; }

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Strategy state machine
        /// </summary>
        /// <param name="strategy"></param>
        public StrategyStateMachine(IStrategy strategy)
        {
            _log = LogProvider.For<StrategyStateMachine>();

            State(() => StrategyApproved);
            State(() => StrategyRevoked);

            Event(() => InvestmentNew, x => x.CorrelateById(os => os.Investment.Id, ctx => ctx.Message.Investment.Id).SelectId(context => Guid.NewGuid()));

            Initially(
                When(InvestmentNew)
                    // Set received Investment to machine state
                    .Then(ctx => ctx.Instance.Investment = ctx.Data.Investment)
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} added to startegy {strategy.Name}")))
                    .IfElse(ctx => strategy.ValidateInvetsment(ctx.Instance.Investment),
                        ctx => ctx.TransitionTo(StrategyApproved),
                        ctx => ctx.TransitionTo(StrategyRevoked)));

            DuringAny(
                // Investment approved by strategy
                When(StrategyApproved.Enter)
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} approved by startegy {strategy.Name}")))
                    // Send approved message
                    .Then(ctx => ctx.Publish<StrategyInvestmentState, IInvestmentStrategyApproved>(new
                    {
                        Investment = ctx.Instance.Investment,
                        StrategyName = strategy.Name
                    }))
                    .Finalize(),

                // Investment revoked by strategy
                When(StrategyRevoked.Enter)
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} revoked by startegy {strategy.Name}")))
                    // Send revoked message
                    .Then(ctx => ctx.Publish<StrategyInvestmentState, IInvestmentStrategyRevoked>(new
                    {
                        Investment = ctx.Instance.Investment,
                        StrategyName = strategy.Name
                    }))
                    .Finalize());

            SetCompletedWhenFinalized();
        }
    }
}
