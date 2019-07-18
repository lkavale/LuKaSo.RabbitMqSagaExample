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
using LuKaSo.RabbitMqSagaExample.Broker.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using LuKaSo.RabbitMqSagaExample.Broker.Jobs;
using LuKaSo.RabbitMqSagaExample.Broker.StateMachine;

namespace LuKaSo.RabbitMqSagaExample.Broker
{
    public class Startup
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration _configuration { get; }

        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _log = LogProvider.For<Startup>();

            _log.Debug($"Application {Assembly.GetAssembly(typeof(Startup)).GetName().Name} starting...");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Broker
            services.AddSingleton<IPortfolioManager, PortfolioManager>();

            // RabbitMQ
            services.AddSingleton<ISagaRepository<InvetsmentBuyerState>, InMemorySagaRepository<InvetsmentBuyerState>>();

            services.AddMassTransit(x =>
            {
                x.AddSagaStateMachine<InvetsmentBuyerStateMachine, InvetsmentBuyerState>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(_configuration.GetSection("RabbitMQ").Get<RabbitMqConfig>());

                    cfg.ReceiveEndpoint(host, "broker", e =>
                    {
                        e.Durable = false;
                        e.ConfigureSaga<InvetsmentBuyerState>(provider);
                    });
                }));
            });

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();

            // Quartz
            services.AddQuartz(new[] { typeof(NewInvestmentJob) });

            // Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Broker API",
                    Description = "Broker API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c => c.RouteTemplate = "api/swagger/{documentname}/swagger.json");

            app.UseMvc();

            // Quartz
            var scheduler = app.ApplicationServices.GetService<IScheduler>();

            QuartzServicesUtilities.StartJob<NewInvestmentJob>(scheduler, TimeSpan.FromSeconds(15));

            // CrystalQuartz dashboard
            app.UseCrystalQuartz(() => scheduler);

            _log.Debug($"Application started {Assembly.GetAssembly(typeof(Startup)).GetName().Name}.");
        }
    }
}
