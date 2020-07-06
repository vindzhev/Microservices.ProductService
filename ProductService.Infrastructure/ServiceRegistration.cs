namespace ProductService.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MicroservicesPOC.Shared;

    using ProductService.Application.Common.Interfaces;

    using ProductService.Infrastructure.Persistance;
    using ProductService.Infrastructure.Persistance.Repositories;

    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ProductDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("ProductServiceConnection"),
                    x => x.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName)))
                .AddScoped<IProductRepository, ProductRepository>();

            services.AddConventionalServices(typeof(ServiceRegistration).Assembly);

            return services;
        }
    }
}
