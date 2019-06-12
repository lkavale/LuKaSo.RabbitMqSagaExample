using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuKaSo.RabbitMqSagaExample.Common;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Common.MassTransit;
using LuKaSo.RabbitMqSagaExample.Strategy;
using MassTransit;
using MassTransit.Saga;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LuKaSo.RabbitMqSagaExample.StrategyB
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStrategy, Strategy>();
            services.AddSingleton<ISagaRepository<StrategyInvestmentState>, InMemorySagaRepository<StrategyInvestmentState>>();

            services.AddMassTransit(x =>
            {
                x.AddSagaStateMachine<StrategyStateMachine, StrategyInvestmentState>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://172.20.0.1/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint(host, "strategyB", e =>
                    {
                        e.Durable = false;
                        e.ConfigureSaga<StrategyInvestmentState>(provider);
                    });
                }));
            });

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
