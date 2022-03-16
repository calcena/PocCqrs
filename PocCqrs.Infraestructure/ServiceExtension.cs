namespace PocCqrs.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using PocCqrs.Domain.Interfaces;
    using PocCqrs.Infrastructure.Repositories;

    public static class ServiceExtension
    {
       public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
