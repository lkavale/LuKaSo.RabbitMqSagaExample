using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement.Models
{
    public class InvestmentCategory
    {
        public double InterestRateFrom { get; set; }

        public double InterestRateTo { get; set; }
    }
}
