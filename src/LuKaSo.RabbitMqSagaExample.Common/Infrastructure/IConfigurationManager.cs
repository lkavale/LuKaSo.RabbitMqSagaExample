using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IConfigurationManager<T>
    {
        T Read();

        void Write(T configuration);
    }
}
