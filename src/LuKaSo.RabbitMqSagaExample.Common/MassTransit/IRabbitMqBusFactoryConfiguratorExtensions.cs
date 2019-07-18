using MassTransit;
using MassTransit.RabbitMqTransport;

namespace LuKaSo.RabbitMqSagaExample.Common.MassTransit
{
    public static class IRabbitMqBusFactoryConfiguratorExtensions
    {
        public static IRabbitMqHost Host(this IRabbitMqBusFactoryConfigurator configurator, RabbitMqConfig config)
        {
            return configurator.Host(config.GetServiceUri(), h =>
            {
                h.Username(config.User);
                h.Password(config.Password);
            });
        }
    }
}
