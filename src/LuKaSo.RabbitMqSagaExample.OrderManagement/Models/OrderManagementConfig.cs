using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement.Models
{
    public class OrderManagementConfig
    {
        public InvestmentCategory InvestmentCategory { get; set; }

        public double Percentage { get; set; }
    }
}
