using LuKaSo.RabbitMqSagaExample.Common.Models;
using System.Collections.Generic;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IOrderManagement
    {
        bool ValidateInvetsment(InvestmentOffer investment);

        void Update(ICollection<Investment> investments);
    }
}
