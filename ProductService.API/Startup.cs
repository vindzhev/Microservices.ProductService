namespace ProductService.API
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    using Steeltoe.Discovery.Client;
    using Newtonsoft.Json.Serialization;

    using ProductService.Application;
    using ProductService.Infrastructure;
    using ProductService.API.Extensions;
    using MicroservicesPOC.Shared.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDiscoveryClient(Configuration);

            //dotnet ef migrations add Initial --project ../ProductService.Infrastructure --context ProductDbContext --output-dir Persistance/Migrations --verbose
            services
                .AddApplication()
                .AddInfrastructure(this.Configuration)
                .AddApiComponents();

            services.AddHealthChecks();

            services
                .AddControllers(setupAction => setupAction.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson(setupAction =>
                {
                    //setupAction.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
                    setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    setupAction.SerializerSettings.Converters.Add(new QuestionAnswerConverter());
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction => setupAction.UseCustomInvalidModelUnprocessableEntityResponse());

            //Register custom 422 api behavior
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else app.UseHsts();

            //app.UseCustomExceptionHandler();
            app.UseHealthChecks("/health");
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseDiscoveryClient();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
