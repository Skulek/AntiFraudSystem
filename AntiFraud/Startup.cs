using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AntiFraud.Orders;
using AntiFraud.Orders.Extensions;
using AntiFraud.Orders.Factories;
using AntiFraud.Orders.Mappings;
using AntiFraud.Orders.Repository;
using AntiFraud.Orders.Services;
using AntiFraud.Products.Repositories;
using AntiFraud.Products.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AntiFraud
{
    public class Startup
    {
        private const string AntiFraudCheckerIntervalInSeconds = "AntiFraudCheckerIntervalInSeconds";
        private const string AntiFraudCheckerDelayStartInSeconds = "AntiFraudCheckerDelayStartInSeconds";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var startDelay = Configuration[AntiFraudCheckerDelayStartInSeconds];
            var interval = Configuration[AntiFraudCheckerIntervalInSeconds];

            services.AddLogging(cfg => cfg.AddConsole());
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(Startup).Assembly); });
            services.AddTransient<IAntiFraudPolicyFactory, AntiFraudPolicyFactory>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IAntiFraudService, AntiFraudService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddDbContext<AntiFraudDbContext>(options => options.UseInMemoryDatabase("Order"), ServiceLifetime.Singleton);
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                q.ScheduleJob<AntiFraudJob>(trigger =>
                {
                    trigger.StartAt(DateTime.Now.AddSeconds(int.Parse(startDelay)));
                    trigger.WithDailyTimeIntervalSchedule(x => x.WithInterval(int.Parse(interval), IntervalUnit.Second));
                });
            });

            services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);

            services.AddHostedService<MigratorHostedService>();
            
            // other config
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
