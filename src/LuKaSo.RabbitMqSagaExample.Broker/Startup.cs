using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using LuKaSo.RabbitMqSagaExample.Common.Quartz;
using MassTransit;
using Quartz;
using CrystalQuartz.AspNetCore;
using LuKaSo.RabbitMqSagaExample.Common.MassTransit;
using MassTransit.Saga;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;

namespace LuKaSo.RabbitMqSagaExample.Broker
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISagaRepository<InvetsmentBuyerState>, InMemorySagaRepository<InvetsmentBuyerState>>();
            services.AddSingleton<IPortfolioManager, PortfolioManager>();

            services.AddMassTransit(x =>
            {
                x.AddSagaStateMachine<InvetsmentBuyerStateMachine, InvetsmentBuyerState>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://172.20.0.1/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint(host, "broker", e =>
                    {
                        e.Durable = false;
                        e.ConfigureSaga<InvetsmentBuyerState>(provider);
                    });
                }));
            });

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();

            services.AddQuartz(new[] { typeof(NewInvestmentJob) });
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

            // Quartz
            var scheduler = app.ApplicationServices.GetService<IScheduler>();

            QuartzServicesUtilities.StartJob<NewInvestmentJob>(scheduler, TimeSpan.FromSeconds(15));

            // CrystalQuartz dashboard
            app.UseCrystalQuartz(() => scheduler);
        }
    }
}
