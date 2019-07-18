using System;

namespace LuKaSo.RabbitMqSagaExample.Common.Models
{
    public class InvestmentOffer
    {
        /// <summary>
        /// Id 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Investment rate
        /// </summary>
        public double InterestRate { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Stringify investment
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Investment ID {Id}, amount {Amount.ToString("F2")}, interest rate {InterestRate.ToString("F2")} %";
        }
    }
}
