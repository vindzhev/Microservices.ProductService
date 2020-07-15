namespace ProductService.Infrastructure
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MicroservicesPOC.Shared.Common;

    using ProductService.Domain.Entities;
    
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

        public static async Task UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ProductDbContext>();
            IProductRepository repository = serviceScope.ServiceProvider.GetService<IProductRepository>();

            await context.Database.MigrateAsync();

            if (!await repository.CheckExists("TRI", CancellationToken.None))
            {
                Product travelInsurance = Product.CreateDraft("TRI", "Safe Traveller", "/assets/travel.jpg", "Travel insurance", 10);
                
                travelInsurance.AddCover("C1", "Illness", string.Empty, false, 5000);
                travelInsurance.AddCover("C2", "Assistance", string.Empty, true, null);

                travelInsurance.AddQuestions(new List<Question>
                {
                    new ChoiceQuestion("DESTINATION", 1, "Destination", new List<Choice>
                    {
                        new Choice("BUL", "Bulgaria"),
                        new Choice("EUR", "Europe"),
                        new Choice("WORLD", "WORLD")
                    })
                });

                travelInsurance.Activate();

                await repository.Add(travelInsurance, CancellationToken.None);
            }

            if (!await repository.CheckExists("HSI", CancellationToken.None))
            {
                Product houseInsurance = Product.CreateDraft("HSI", "Happy House", "/assets/house.jpg", "House insurance", 5);

                houseInsurance.AddCover("C1", "Fire", string.Empty, false, 200000);
                houseInsurance.AddCover("C2", "Flood", string.Empty, false, 100000);
                houseInsurance.AddCover("C3", "Theft", string.Empty, false, 50000);
                houseInsurance.AddCover("C4", "Assistance", string.Empty, true, null);

                houseInsurance.AddQuestions(new List<Question>
                {
                    new ChoiceQuestion("TYP", 1, "What's the real estate type?", new List<Choice>
                    {
                        new Choice("APT", "Apartment"),
                        new Choice("HOUSE", "House")
                    }),
                    new NumericQuestion("AREA", 2, "What's the area of the real estate?"),
                    new NumericQuestion("NUM_OF_CLAIMS", 3, "Number of claims in the past 5 yers"),
                    new ChoiceQuestion("FLOOD", 4, "Is the property located in flood risk area?", ChoiceQuestion.YesNoChoice())
                });

                houseInsurance.Activate();

                await repository.Add(houseInsurance, CancellationToken.None);
            }

            if (!await repository.CheckExists("FAI", CancellationToken.None))
            {
                Product farmInsurance = Product.CreateDraft("FAI", "Happy Farm", "/assets/farm.jpg", "Farm insurance", 1);

                farmInsurance.AddCover("C1", "Crops", string.Empty, false, 200000);
                farmInsurance.AddCover("C2", "Flood", string.Empty, false, 100000);
                farmInsurance.AddCover("C3", "Fire", string.Empty, false, 50000);
                farmInsurance.AddCover("C4", "Equipment", string.Empty, true, 300000);

                farmInsurance.AddQuestions(new List<Question> 
                {
                    new ChoiceQuestion("TYP", 1, "Cultivation type", new List<Choice> 
                    {
                            new Choice("ZB", "Crop"),
                            new Choice("KW", "Vegetable")
                    }),
                    new NumericQuestion("AREA", 2, "Area"),
                    new NumericQuestion("NUM_OF_CLAIM", 3, "Number of claims in the past 5 years"),
                });

                farmInsurance.Activate();

                await repository.Add(farmInsurance, CancellationToken.None);
            }

            if (!await repository.CheckExists("CAR", CancellationToken.None))
            {
                Product carInsurance = Product.CreateDraft("CAR", "Happy Driver", "/assets/car.jpg", "Car insurance", 1);

                carInsurance.AddCover("C1", "Assistance", string.Empty, true, null);

                carInsurance.AddQuestions(new List<Question> {
                    new NumericQuestion("NUM_OF_CLAIM", 3, "Number of claims in the past 5 years")
                });

                carInsurance.Activate();

                await repository.Add(carInsurance, CancellationToken.None);
            }

            await repository.SaveChangesAsync();
        }
    }
}
