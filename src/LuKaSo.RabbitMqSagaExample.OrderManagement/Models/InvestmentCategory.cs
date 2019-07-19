using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement.Models
{
    public class InvestmentCategory
    {
        /// <summary>
        /// Interest rate from
        /// </summary>
        public double InterestRateFrom { get; set; }

        /// <summary>
        /// Interest rate to
        /// </summary>
        public double InterestRateTo { get; set; }
    }
}
