using System;

namespace LuKaSo.RabbitMqSagaExample.Models
{
    [Serializable]
    public class Investment
    {
        public Guid Id { get; set; }

        public double InterestRate { get; set; }

        public decimal Amount { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"Investment ID {Id}, amount {Amount.ToString("F2")}, interest rate {InterestRate.ToString("F2")} %";
        }
    }
}
