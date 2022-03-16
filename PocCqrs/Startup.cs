namespace PocCqrs
{
    using AutoMapper;
    using MassTransit;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using PocCqrs.Aplication.MessageBroker.Consumer;
    using PocCqrs.Application.Mapper;
    using PocCqrs.Application.Products.Queries;
    using PocCqrs.Infrastructure;
    using PocCqrs.Infrastructure.EFCore;
    using Serilog;
    using Serilog.Events;
    using Serilog.Extensions.Logging;
    using Serilog.Sinks.Elasticsearch;
    using System;
    using System.Reflection;

    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
       {
            #region Serilog
            services.AddLogging(logBuilder => {
                var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "/Logs/PocCqrs_.log",
                outputTemplate: "{Timestamp:HH:mm:ss} - {Level: u3} - {Message} {Properties}{NewLine}",
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: 20,
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 100000
                )
                #region SEQ configuration
                .WriteTo.Seq(Configuration.GetConnectionString("SeqConnection"))
                #endregion
                #region Elasticsearch configuration
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration.GetConnectionString("ElasticConnection")))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    IndexFormat = $"PocCQRS_Logs_{DateTime.Now:yyyy_MM_dd}",
                    NumberOfReplicas = 0                    
                })
                #endregion
                .Enrich.WithProperty("Environment", _env.EnvironmentName);
                var logger = loggerConfiguration.CreateLogger();

                logBuilder.Services.AddSingleton<ILoggerFactory>(
                    provider => new SerilogLoggerFactory(logger, dispose: false));
            });
            #endregion

            #region MassTransit - RMQ
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductConsumer>();
                x.UsingRabbitMq((ctx, config) => {
                    config.Host(Configuration.GetConnectionString("RmqConnection"));
                    config.ReceiveEndpoint("productQueue", c =>
                    {
                        c.PrefetchCount = 20;
                        c.Durable = true;
                        c.ConfigureConsumer<ProductConsumer>(ctx);
                    });
                });             
            });
            services.AddMassTransitHostedService();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PoC CQRS", Version = "v1" });
                #region Security Swagger with ApiKey
                //c.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
                //c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});
                #endregion
            });
            services.AddAutoMapper(typeof(Startup));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddInfrastructure();

            #region DBContext
            services.AddDbContext<EFDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MsqlConnection")));
            #endregion

            #region MediatR
            services.AddMediatR(new Assembly[] { Assembly.GetExecutingAssembly(),typeof(GetAllProductsQuery).Assembly});
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PoC CQRS v1"));
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
