using System;
using System.Globalization;

namespace LuKaSo.RabbitMqSagaExample.Common.Models
{
    [Serializable]
    public class Investment
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
        /// Invested
        /// </summary>
        public DateTime InvestmentTime { get; set; }

        /// <summary>
        /// Investment
        /// </summary>
        public Investment()
        {
        }

        /// <summary>
        /// Investment
        /// </summary>
        /// <param name="offer"></param>
        public Investment(InvestmentOffer offer)
        {
            Id = offer.Id;
            InterestRate = offer.InterestRate;
            Amount = offer.Amount;
            InvestmentTime = DateTime.Now;
        }

        /// <summary>
        /// Stringify investment
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Investment ID {Id}, invested at {InvestmentTime.ToString(CultureInfo.InvariantCulture)}, amount {Amount.ToString("F2")}, interest rate {InterestRate.ToString("F2")} %";
        }
    }
}
