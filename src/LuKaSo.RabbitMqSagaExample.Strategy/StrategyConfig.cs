namespace LuKaSo.RabbitMqSagaExample.Strategy
{
    public class StrategyConfig
    {
        /// <summary>
        /// Interest rate from
        /// </summary>
        public double? InterestRateFrom { get; set; }

        /// <summary>
        /// Interest rate ti
        /// </summary>
        public double? InterestRateTo { get; set; }

        /// <summary>
        /// Amount from
        /// </summary>
        public decimal? AmountFrom { get; set; }

        /// <summary>
        /// Amount to
        /// </summary>
        public decimal? AmountTo { get; set; }
    }
}
