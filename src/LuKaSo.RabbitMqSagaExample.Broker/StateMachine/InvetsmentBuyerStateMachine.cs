using Automatonymous;
using LuKaSo.RabbitMqSagaExample.Broker.Logging;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Contracts.Broker;
using LuKaSo.RabbitMqSagaExample.Contracts.OrderManagement;
using LuKaSo.RabbitMqSagaExample.Contracts.Strategy;
using System;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.Broker.StateMachine
{
    public class InvetsmentBuyerStateMachine : MassTransitStateMachine<InvetsmentBuyerState>
    {
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        public InvetsmentBuyerStateMachine(IPortfolioManager portfolioManager)
        {
            _log = LogProvider.For<InvetsmentBuyerStateMachine>();

            State(() => WaitingForResponce);

            Event(() => InvestmentNew, x => x.CorrelateById(os => os.Investment.Id, ctx => ctx.Message.Investment.Id).SelectId(context => Guid.NewGuid()));

            Event(() => InvetsmentStrategyApproved, x => x.CorrelateById(os => os.Investment.Id, ctx => ctx.Message.Investment.Id));
            Event(() => InvetsmentStrategyRevoked, x => x.CorrelateById(os => os.Investment.Id, ctx => ctx.Message.Investment.Id));

            Event(() => OrderManagementApproved, x => x.CorrelateById(os => os.Investment.Id, ctx => ctx.Message.Investment.Id));
            Event(() => OrderManagementRevoked, x => x.CorrelateById(os => os.Investment.Id, ctx => ctx.Message.Investment.Id));

            CompositeEvent(() => InvestmentApproved, x => x.BuyApprovementState, InvetsmentStrategyApproved, OrderManagementApproved);

            Initially(
                When(InvestmentNew)
                    // Set received Investment to machine state
                    .Then(ctx => ctx.Instance.Investment = ctx.Data.Investment)
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} added to buyer")))
                    .TransitionTo(WaitingForResponce));

            During(WaitingForResponce,
                Ignore(InvetsmentStrategyRevoked),

                When(InvestmentApproved)
                    // Add investment into portfolio
                    .Then(ctx => portfolioManager.Add(ctx.Instance.Investment))
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} bought into portfolio")))
                    // Publish new portfolio
                    .Then(ctx => ctx.Publish<InvetsmentBuyerState, IPortfolio>(new { Investments = portfolioManager.GetInvestments() }))
                    .Finalize(),

                When(OrderManagementRevoked)
                    // Log
                    .ThenAsync(ctx => Task.Run(() => _log.Debug($"Investment {ctx.Instance.Investment.ToString()} revoked")))
                    .Finalize());

            SetCompletedWhenFinalized();
        }

        /// <summary>
        /// State waiting for responce from decision process
        /// </summary>
        public State WaitingForResponce { get; private set; }

        /// <summary>
        /// New investment occured on bus
        /// </summary>
        public Event<IInvestmentOfferNew> InvestmentNew { get; private set; }

        /// <summary>
        /// Composite event investment approved
        /// </summary>
        public Event InvestmentApproved { get; private set; }

        /// <summary>
        /// Strategy approved the investment
        /// </summary>
        public Event<IInvestmentOfferStrategyApproved> InvetsmentStrategyApproved { get; private set; }

        /// <summary>
        /// Strategy revoked the investment
        /// </summary>
        public Event<IInvestmentOfferStrategyRevoked> InvetsmentStrategyRevoked { get; private set; }

        /// <summary>
        /// Order management approved the investment
        /// </summary>
        public Event<IInvestmentOfferOrderManagementApproved> OrderManagementApproved { get; private set; }

        /// <summary>
        /// Order management revoked the investment
        /// </summary>
        public Event<IInvestmentOfferOrderManagementRevoked> OrderManagementRevoked { get; private set; }
    }
}
