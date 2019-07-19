using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.RabbitMqSagaExample.Common.Infrastructure
{
    public interface IConfigurationManager<T>
    {
        /// <summary>
        /// Read configuration
        /// </summary>
        /// <returns></returns>
        T Read();

        /// <summary>
        /// Write configuration
        /// </summary>
        /// <param name="configuration"></param>
        void Write(T configuration);
    }
}
