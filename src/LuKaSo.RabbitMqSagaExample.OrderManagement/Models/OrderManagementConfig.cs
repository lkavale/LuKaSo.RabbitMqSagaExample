using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuKaSo.RabbitMqSagaExample.OrderManagement.Models
{
    public class OrderManagementConfig
    {
        /// <summary>
        /// Investment category
        /// </summary>
        public InvestmentCategory InvestmentCategory { get; set; }

        /// <summary>
        /// Allowed percentage in portfolio
        /// </summary>
        public double Percentage { get; set; }
    }
}
