using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Common.Models;
using LuKaSo.RabbitMqSagaExample.OrderManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement
{
    public class OrderManagement : IOrderManagement
    {
        /// <summary>
        /// Configuration manager
        /// </summary>
        private readonly IConfigurationManager<List<OrderManagementConfig>> _configManager;

        /// <summary>
        /// Free amounts in categories
        /// </summary>
        private volatile IDictionary<InvestmentCategory, decimal> _freeAmounts = new Dictionary<InvestmentCategory, decimal>();

        /// <summary>
        /// Possible overlap
        /// </summary>
        private const decimal _possibleOverlap = 2000;

        /// <summary>
        /// Lock
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Order management
        /// </summary>
        /// <param name="configManager"></param>
        public OrderManagement(IConfigurationManager<List<OrderManagementConfig>> configManager)
        {
            _configManager = configManager;

            _freeAmounts = _configManager.Read()
                .Select(c => new
                {
                    Key = c.InvestmentCategory,
                    Value = CalculateAllowedAmount(c.Percentage, 0)
                })
                .ToDictionary(i => i.Key, i => i.Value);
        }

        /// <summary>
        /// Validate investments
        /// </summary>
        /// <param name="investment"></param>
        /// <returns></returns>
        public bool ValidateInvetsment(InvestmentOffer investment)
        {
            var isValid = false;

            lock (_lock)
            {
                var category = _freeAmounts.Keys.Single(f => MatchCategory(f, investment.InterestRate));

                if (category != null)
                {
                    isValid = _freeAmounts[category] > investment.Amount;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Update free amounts in investments 
        /// </summary>
        /// <param name="investments"></param>
        public void Update(ICollection<Investment> investments)
        {
            var freeAmounts = _configManager.Read()
                .Select(c =>
                {
                    var amount = investments
                        .Where(i => MatchCategory(c.InvestmentCategory, i.InterestRate))
                        .Sum(i => i.Amount);

                    return new
                    {
                        Key = c.InvestmentCategory,
                        Value = CalculateAllowedAmount(c.Percentage, investments.Sum(i => i.Amount)) - amount
                    };
                })
                .ToDictionary(i => i.Key, i => i.Value);

            lock (_lock)
            {
                _freeAmounts = freeAmounts;
            }
        }

        /// <summary>
        /// Match investment into 
        /// </summary>
        /// <param name="investmentCategory"></param>
        /// <param name="interestRate"></param>
        /// <returns></returns>
        protected bool MatchCategory(InvestmentCategory investmentCategory, double interestRate)
        {
            // i => c.InvestmentCategory.InterestRateFrom <= i.InterestRate && c.InvestmentCategory.InterestRateTo > i.InterestRate)
            return investmentCategory.InterestRateFrom <= interestRate && investmentCategory.InterestRateTo > interestRate;
        }

        /// <summary>
        /// Calculate allowed amount
        /// </summary>
        /// <param name="percentage"></param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        protected decimal CalculateAllowedAmount(double percentage, decimal totalAmount)
        {
            if (percentage > 0)
            {
                return (decimal)(percentage * (double)totalAmount) + _possibleOverlap;
            }

            return 0;
        }
    }
}
