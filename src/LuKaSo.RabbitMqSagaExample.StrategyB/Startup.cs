using LuKaSo.RabbitMqSagaExample.Common.Configuration;
using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using LuKaSo.RabbitMqSagaExample.Common.MassTransit;
using LuKaSo.RabbitMqSagaExample.Strategy;
using LuKaSo.RabbitMqSagaExample.StrategyB.Logging;
using MassTransit;
using MassTransit.Saga;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;

namespace LuKaSo.RabbitMqSagaExample.StrategyB
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddApplicationPart(AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "LuKaSo.RabbitMqSagaExample.Strategy"));

            // Strategy
            services.AddSingleton<IConfigurationManager<StrategyConfig>>(new FileConfigurationManager<StrategyConfig>("strategyConfig.json"));
            services.AddSingleton<IStrategy, Strategy>();

            // RabbitMQ
            services.AddSingleton<ISagaRepository<StrategyInvestmentState>, InMemorySagaRepository<StrategyInvestmentState>>();

            services.AddMassTransit(x =>
            {
                x.AddSagaStateMachine<StrategyStateMachine, StrategyInvestmentState>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(_configuration.GetSection("RabbitMQ").Get<RabbitMqConfig>());

                    cfg.ReceiveEndpoint(host, "strategyB", e =>
                    {
                        e.Durable = false;
                        e.ConfigureSaga<StrategyInvestmentState>(provider);
                    });
                }));
            });

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();

            // Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Strategy B API",
                    Description = "Strategy B API"
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

            _log.Debug($"Application started {Assembly.GetAssembly(typeof(Startup)).GetName().Name}.");
        }
    }
}
